DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_USUARIO] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_USUARIO
	@Opcion				INT,
	@Id					INT = 0,
	@CUI				NVARCHAR(15) = NULL,
	@Nombre				NVARCHAR(75) = NULL,
	@Apellido			NVARCHAR(75) = NULL,
	@Password			NVARCHAR(500) = NULL,
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_USUARIO

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM USUARIO;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO USUARIO ([CUI], [Nombre], [Apellido], [Password], [AuditUsuarioCreacion]) 
			VALUES (@CUI, @Nombre, @Apellido, @Password, @Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE USUARIO SET [CUI] = @CUI, [Nombre] = @Nombre, 
			[Apellido] = @Apellido, [Password] = @Password, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM USUARIO WHERE [Id] = @Id; 
		END
		
		IF (@Opcion = 5) --Opción para eliminar
		BEGIN
			SELECT * FROM USUARIO WHERE [CUI] = @CUI;
		END
		
		IF (@Opcion = 6) --Opción para eliminar
		BEGIN
			SELECT * FROM USUARIO WHERE [CUI] = @CUI AND [Password] = @Password;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_USUARIO
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_USUARIO
		SET @Respuesta = ERROR_MESSAGE()
	END CATCH
END
GO
