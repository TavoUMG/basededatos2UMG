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
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_CATEGORIA

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM CATEGORIA;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO CATEGORIA ([Nombre],[AuditUsuarioCreacion]) 
			VALUES (@Nombre,@Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE CATEGORIA SET [Nombre] = @Nombre, [AuditUsuarioCreacion] = @Usuario 			
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para seleccionar por id
		BEGIN
			DELETE FROM CATEGORIA WHERE [Id] = @Id; 
		END		
		
		IF (@Opcion = 5) --Opci�n para eliminar
		BEGIN
			SELECT * FROM CATEGORIA WHERE [Id] = @Id; 
		END	

		COMMIT TRAN TRAN_MATENIMIENTO_CATEGORIA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_CATEGORIA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
