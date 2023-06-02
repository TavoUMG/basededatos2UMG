DROP PROCEDURE IF EXISTS [dbo].[SP_TRANSACCION_FACTURA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_TRANSACCION_FACTURA
	@ClienteId			INT,
	@Cui_Nit			NVARCHAR(15),
	@Direccion			NVARCHAR(250) NULL,
	@Fecha				DATETIME,
	@Total				DECIMAL(20,2),
	@Detalle			NVARCHAR(MAX),
	@Monto				DECIMAL(20,2),
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_TRANSACCION_FACTURA

			DECLARE @MontoRestante DECIMAL(20,2) = (@Total - @Monto);

			IF @MontoRestante < 0
			BEGIN
				DECLARE @Mensaje NVARCHAR(MAX) = N'El monto que paga Q '+CONVERT(VARCHAR, @Monto)+' supera al monto total de la factura Q '+CONVERT(VARCHAR, @Total);
				RAISERROR (1, -1, -1, @Mensaje);
			END

			INSERT INTO FACTURA ([ClienteId], [CUI_NIT], [Direccion], [Fecha], [Total], [AuditUsuarioCreacion]) 
			VALUES (@ClienteId, @Cui_Nit, @Direccion, @Fecha, @Total, @Usuario);

			DECLARE @FacturaId INT = (SELECT IDENT_CURRENT('FACTURA'));

			DECLARE @i INT = 0;
			DECLARE @detalleJSON TABLE(
				[Id] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
				[Cadena] [nvarchar](MAX) NOT NULL
			);
			
			INSERT INTO @detalleJSON
			SELECT VALUE FROM OPENJSON(@Detalle,'$.Detalle');
        
			WHILE (SELECT COUNT(*) FROM @detalleJSON) > 0
			BEGIN
				SET @i = @i + 1;
				DECLARE @Cadena NVARCHAR(MAX) = (SELECT CADENA FROM @detalleJSON WHERE [Id] = @i);

				DECLARE 
					@ProductoId INT = (SELECT VALUE FROM OPENJSON(@Cadena) WHERE [KEY] = 'ProductoId'),
					@Cantidad INT = (SELECT VALUE FROM OPENJSON(@Cadena) WHERE [KEY] = 'Cantidad'),
					@Precio DECIMAL(20, 2) = (SELECT VALUE FROM OPENJSON(@Cadena) WHERE [KEY] = 'Precio'),
					@SubTotal DECIMAL(20, 2) = (SELECT VALUE FROM OPENJSON(@Cadena) WHERE [KEY] = 'SubTotal') 

				IF (SELECT STOCK FROM INVENTARIO WHERE ProductoId = @ProductoId) > (@Cantidad - 1)
				BEGIN
					INSERT INTO DETALLE ([FacturaId], [ProductoId], [Cantidad], [Precio], [SubTotal], [AuditUsuarioCreacion]) 
					VALUES (@FacturaId, @ProductoId, @Cantidad, @Precio, @SubTotal, @Usuario);
				END
				ELSE
				BEGIN
					DECLARE @ProductoName NVARCHAR(MAX) = (SELECT 'El producto '+CONVERT(VARCHAR, Id)+' - '+Nombre+' no cuenta con el stock suficiente.' FROM PRODUCTO WHERE Id = @ProductoId);
					RAISERROR (1, -1, -1, @ProductoName);
				END

				DELETE FROM @detalleJSON WHERE [Id] = @i;
			END

			DECLARE @UsuarioId INT = (SELECT Id FROM USUARIO WHERE UPPER(REPLACE(CONCAT(Nombre, Apellido),' ','')) = UPPER(REPLACE(@Usuario,' ','')));
			INSERT INTO COBRO ([MontoReal], [Monto], [MontoRestante], [FacturaId], [UsuarioId]) 
			VALUES (@Total, @Monto, @MontoRestante, @FacturaId, @UsuarioId);

			SELECT FAC.*
			FROM FACTURA AS FAC
			WHERE FAC.ID = @FacturaId;

			SELECT DET.*
			FROM DETALLE AS DET
			WHERE DET.FacturaId = @FacturaId;
		COMMIT TRAN TRAN_TRANSACCION_FACTURA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_TRANSACCION_FACTURA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
