DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_INVENTARIO] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_INVENTARIO
	@Opcion				INT,
	@Id					INT = 0,
	@ProductoId			INT,
	@PrecioVenta		DECIMAL(20,2),
	@Stock				INT,	
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_INVENTARIO

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM INVENTARIO;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO INVENTARIO ([ProductoId],[PrecioVenta],[Stock],[AuditUsuarioCreacion]) 
			VALUES (@ProductoId	, @PrecioVenta, @Stock, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE INVENTARIO SET [ProductoId]=@ProductoId, [PrecioVenta]=@PrecioVenta, [Stock]=@Stock, 
			 [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM INVENTARIO WHERE [Id] = @Id; 
		END

		IF (@Opcion = 5) --Opci�n para seleccionar uno por ID
		BEGIN
			SELECT * FROM INVENTARIO WHERE [Id] = @Id;
		END

		IF (@Opcion = 6) --Opci�n para seleccionar uno por ProductoId
		BEGIN
			SELECT * FROM INVENTARIO WHERE [ProductoId] = @ProductoId;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_INVENTARIO
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_INVENTARIO
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
