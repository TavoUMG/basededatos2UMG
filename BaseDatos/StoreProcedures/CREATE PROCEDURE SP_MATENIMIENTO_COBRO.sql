DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_COBRO] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_COBRO
	@Opcion				INT,
	@Id					INT = 0,
	@Monto				DECIMAL(20,2),
	@FacturaId			INT = 0,
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		DECLARE @UsuarioId INT, @Total DECIMAL(20,2)

		BEGIN TRAN TRAN_MATENIMIENTO_COBRO

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM COBRO;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			SET @Total = (SELECT TOP 1 [MontoRestante] FROM COBRO WHERE [FacturaId] = @FacturaId ORDER BY [Id] DESC);
			SET @UsuarioId = (SELECT [Id] FROM USUARIO WHERE UPPER(REPLACE(CONCAT(Nombre, Apellido),' ','')) = UPPER(REPLACE(@Usuario,' ','')));

			DECLARE @MontoRestante DECIMAL(20,2) = (@Total - @Monto);

			IF @MontoRestante < 0
			BEGIN
				DECLARE @Mensaje NVARCHAR(MAX) = N'El monto que paga Q '+CONVERT(VARCHAR, @Monto)+' supera al monto total de la factura Q '+CONVERT(VARCHAR, @Total);
				RAISERROR (1, -1, -1, @Mensaje);
			END

			INSERT INTO COBRO ([MontoReal], [Monto], [MontoRestante], [FacturaId], [UsuarioId]) 
			VALUES (@Total, @Monto, @MontoRestante, @FacturaId, @UsuarioId);
		END
		
		IF (@Opcion = 3) --Opci�n para eliminar
		BEGIN
			DELETE FROM COBRO WHERE [Id] = @Id; 
		END

		IF (@Opcion = 4) --Opci�n para seleccionar uno por ID
		BEGIN
			SELECT * FROM COBRO WHERE [Id] = @Id;
		END

		IF (@Opcion = 5) --Opci�n para seleccionar Facturas pendientes de pago
		BEGIN
			SELECT * FROM FACTURA WHERE [Pagado] = 0;
		END

		IF (@Opcion = 6) --Opci�n para seleccionar monto restante de la Factura
		BEGIN
			SELECT TOP 1 * FROM COBRO WHERE [FacturaId] = @FacturaId ORDER BY [Id] DESC;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_COBRO
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_COBRO
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
