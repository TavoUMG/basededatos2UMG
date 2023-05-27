DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_DEVOLUCION] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_DEVOLUCION
	@Opcion				INT,
	@Id					INT = 0,
	@Cantidad			INT,
	@FacturaId			INT,
	@DetalleId			INT,
	@ProductoId			INT,
	@Fecha				DATETIME = NULL,
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_DEVOLUCION

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM DEVOLUCION;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO DEVOLUCION ([Cantidad], [FacturaId], [DetalleId], [ProductoId], [Fecha], [AuditUsuarioCreacion]) 
			VALUES (@Cantidad, @FacturaId, @DetalleId, @ProductoId, @Fecha, @Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE DEVOLUCION SET [Cantidad]=@Cantidad, [FacturaId]=@FacturaId,[DetalleId]=@DetalleId, [ProductoId]=@ProductoId, [Fecha]=@Fecha,
			 [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM DEVOLUCION WHERE [Id] = @Id; 
		END

		IF (@Opcion = 5) --Opción para seleccionar uno por ID
		BEGIN
			SELECT * FROM DEVOLUCION WHERE [Id] = @Id;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_DEVOLUCION
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_DEVOLUCION
		SET @Respuesta = ERROR_MESSAGE()
	END CATCH
END
GO
