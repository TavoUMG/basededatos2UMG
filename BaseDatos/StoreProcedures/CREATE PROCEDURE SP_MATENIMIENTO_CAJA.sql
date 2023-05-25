DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_CAJA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_CAJA
	@Opcion				INT,
	@Id					INT = 0,
	@UsuarioId			INT,
	@efectivoApertura	Decimal(20,2) = NULL,
	@efectivoCierre		Decimal(20,2) = NULL,
	@Usuario			NVARCHAR(150),
	@Respuesta			NVARCHAR(MAX) = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_CAJA

		IF (@Opcion = 1) --Opción para devolver el listado completo
		BEGIN
			SELECT * FROM CAJA;
		END
		
		IF (@Opcion = 2) --Opción para crear
		BEGIN
			INSERT INTO CAJA ([UsuarioId],[efectivoApertura], [efectivoCierre], [AuditUsuarioCreacion] ) 
			VALUES (@UsuarioId, @efectivoApertura, @efectivoCierre, @Usuario);
		END
		
		IF (@Opcion = 3) --Opción para actualizar
		BEGIN
			UPDATE CAJA SET [UsuarioId]= @UsuarioId, [efectivoApertura] = @efectivoApertura, 
			[efectivoCierre] = @efectivoCierre, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opción para eliminar
		BEGIN
			DELETE FROM CAJA WHERE [Id] = @Id; 
		END		

		COMMIT TRAN TRAN_MATENIMIENTO_CAJA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_CAJA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
