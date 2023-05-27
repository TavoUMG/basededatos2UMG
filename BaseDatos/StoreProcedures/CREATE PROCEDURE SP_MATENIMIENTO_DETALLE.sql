DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_DETALLE] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_DETALLE
	@Opcion				INT,
	@Id					INT = 0,
	@FacturaId			INT,
	@ProductoId			INT,
	@Cantidad			INT = NULL,
	@Precio				DECIMAL(20,2),
	@SubTotal			DECIMAL(20,2),
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_DETALLE

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM DETALLE;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO DETALLE ([FacturaId], [ProductoId], [Cantidad], [Precio], [SubTotal], [AuditUsuarioCreacion]) 
			VALUES (@FacturaId, @ProductoId, @Cantidad, @Precio, @SubTotal, @Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE DETALLE SET [FacturaId]=@FacturaId, [ProductoId]=@ProductoId, [Cantidad]=@Cantidad, [Precio]=@Precio, [SubTotal]=@SubTotal,
			 [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM DETALLE WHERE [Id] = @Id; 
		END

		IF (@Opcion = 5) --Opción para seleccionar uno por ID
		BEGIN
			SELECT * FROM DETALLE WHERE [Id] = @Id;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_DETALLE
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_DETALLE
		SET @Respuesta = ERROR_MESSAGE()
	END CATCH
END
GO
