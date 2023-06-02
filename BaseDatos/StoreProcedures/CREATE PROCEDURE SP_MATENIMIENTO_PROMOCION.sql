DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_PROMOCION] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_PROMOCION
	@Opcion				INT,
	@Id					INT = 0,
	@Descripcion		NVARCHAR(MAX),
	@ProductoId			INT = 0,
	@TipoPromocionId	INT = 0,
	@ProductosId		NVARCHAR(MAX),
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_PROMOCION

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM PROMOCION;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO PROMOCION ([Descripcion], [ProductoId], [TipoPromocionId], [AuditUsuarioCreacion]) 
			VALUES (@Descripcion, @ProductoId, @TipoPromocionId, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE PROMOCION SET [Descripcion]=@Descripcion, [ProductoId]=@ProductoId, [TipoPromocionId]=@TipoPromocionId, 
			[AuditUsuarioCreacion]=@Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM PROMOCION WHERE [Id] = @Id; 
		END		

		IF (@Opcion = 5) --Opci�n para devolver productos de una promocion
		BEGIN
			SELECT * FROM PROMOCION WHERE [ProductoId] IN (@ProductosId) AND [TipoPromocionId] = @TipoPromocionId;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_PROMOCION
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_PROMOCION
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
