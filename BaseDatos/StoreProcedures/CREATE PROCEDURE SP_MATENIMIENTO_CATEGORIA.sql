DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_CATEGORIA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_CATEGORIA
	@Opcion				INT,
	@Id					INT = 0,	
	@Nombre				NVARCHAR(75) = NULL,	
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_CATEGORIA

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM CATEGORIA;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO CATEGORIA ([Nombre],[AuditUsuarioCreacion]) 
			VALUES (@Nombre,@Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE CATEGORIA SET [Nombre] = @Nombre, [AuditUsuarioCreacion] = @Usuario 			
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM CATEGORIA WHERE [Id] = @Id; 
		END	

		COMMIT TRAN TRAN_MATENIMIENTO_CATEGORIA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_CATEGORIA
		SET @Respuesta = ERROR_MESSAGE()
	END CATCH
END
GO
