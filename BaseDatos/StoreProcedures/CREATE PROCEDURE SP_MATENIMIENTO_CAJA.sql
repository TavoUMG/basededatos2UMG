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

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM CAJA;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO CAJA ([UsuarioId], [efectivoApertura], [efectivoCierre], [AuditUsuarioCreacion]) 
			VALUES (@UsuarioId, @efectivoApertura, 0, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE CAJA SET [UsuarioId]= @UsuarioId, [efectivoApertura] = @efectivoApertura, 
			[efectivoCierre] = @efectivoCierre, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM CAJA WHERE [Id] = @Id; 
		END	
		
		IF (@Opcion = 5) --Opci�n para preguntar si existe caja abierta
		BEGIN
			SELECT * FROM CAJA 
			WHERE [UsuarioId] = @UsuarioId 
			AND [efectivoCierre] = 0;
		END	
		
		IF (@Opcion = 6) --Opci�n para cerrar la caja
		BEGIN
			UPDATE CAJA 
			SET [efectivoCierre] = @efectivoCierre
			WHERE [Id] = @Id;
		END	

		IF (@Opcion = 7) --Opci�n para seleccionar por id
		BEGIN
			SELECT * FROM CAJA WHERE [Id] = @Id;
		END	

		COMMIT TRAN TRAN_MATENIMIENTO_CAJA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_CAJA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
