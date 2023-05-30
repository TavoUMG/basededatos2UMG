DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_PROVEEDOR] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_PROVEEDOR
	@Opcion				INT,
	@Id					INT = 0,
	@Nombre				NVARCHAR(75) = NULL,
	@Direccion			NVARCHAR(75)= NULL,
	@Telefono			NVARCHAR(25) = NULL,	
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_PROVEEDOR

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM PROVEEDOR;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO PROVEEDOR ([Nombre],[Direccion], [Telefono], [AuditUsuarioCreacion]) 
			VALUES (@Nombre, @Direccion, @Telefono, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE PROVEEDOR SET [Nombre] = @Nombre, [Direccion] = @Direccion,
			 [Telefono] = @Telefono, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM PROVEEDOR WHERE [Id] = @Id; 
		END

		IF (@Opcion = 5) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM PROVEEDOR WHERE [Id] = @Id;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_PROVEEDOR
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_PROVEEDOR
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
