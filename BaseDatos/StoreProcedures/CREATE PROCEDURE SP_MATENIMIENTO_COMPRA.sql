DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_COMPRA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_COMPRA
	@Opcion				INT,
	@Id					INT = 0,
	@Cantidad			INT,
	@ProductoId			INT,
	@ProveedorId		INT,
	@PrecioCosto		DECIMAL(20,2),	
	@CajaId				INT = 0,
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_COMPRA

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM COMPRA;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO COMPRA([Cantidad], [ProductoId],[ProveedorId], [PrecioCosto], [CajaId], [AuditUsuarioCreacion] ) 
			VALUES (@Cantidad, @ProductoId, @ProveedorId, @PrecioCosto, @CajaId, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE COMPRA SET [Cantidad]=@Cantidad, [ProductoId]=@ProductoId, [ProveedorId]=@ProveedorId, 
			[PrecioCosto]=@PrecioCosto, [AuditUsuarioCreacion]=@Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM COMPRA WHERE [Id] = @Id; 
		END		

		IF (@Opcion = 5) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM COMPRA WHERE [Id] = @Id;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_COMPRA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_COMPRA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
