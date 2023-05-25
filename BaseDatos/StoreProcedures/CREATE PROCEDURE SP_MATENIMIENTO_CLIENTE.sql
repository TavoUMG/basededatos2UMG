DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_CLIENTE] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_CLIENTE
	@Opcion				INT,
	@Id					INT = 0,
	@Nombre				NVARCHAR(75) = NULL,
	@Apellido			NVARCHAR(75) = NULL,
	@Telefono			NVARCHAR(20) = NULL,
	@Direccion			NVARCHAR(500)= NULL,
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_CLIENTE

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM CLIENTE;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO CLIENTE ([Nombre],[Apellido], [Telefono], [Direccion], [AuditUsuarioCreacion]) 
			VALUES (@Nombre, @Apellido, @Telefono, @Direccion, @Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE CLIENTE SET [Nombre] = @Nombre, 
			[Apellido] = @Apellido, [Telefono] = @Telefono,[Direccion] = @Direccion, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM CLIENTE WHERE [Id] = @Id; 
		END

		COMMIT TRAN TRAN_MATENIMIENTO_CLIENTE
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_CLIENTE
		SET @Respuesta = ERROR_MESSAGE()
	END CATCH
END
GO
