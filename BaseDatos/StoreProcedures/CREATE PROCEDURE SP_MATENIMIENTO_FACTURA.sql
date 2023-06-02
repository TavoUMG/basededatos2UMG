DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_FACTURA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_FACTURA
	@Opcion				INT,
	@Id					INT = 0,
	@ClienteId			INT,
	@Cui_Nit			NVARCHAR(15) = NULL,
	@Direccion			NVARCHAR(250) = NULL,
	@Fecha				DATETIME = NULL,
	@Total				DECIMAL(20,2),
	@CajaId				INT = 0,
	@Usuario			NVARCHAR(150)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN TRAN_MATENIMIENTO_FACTURA

		IF (@Opcion = 1) --Opci�n para devolver el listado completo
		BEGIN
			SELECT * FROM FACTURA;
		END
		
		IF (@Opcion = 2) --Opci�n para crear
		BEGIN
			INSERT INTO FACTURA ([ClienteId], [CUI_NIT], [Direccion], [Fecha], [Total], [CajaId], [AuditUsuarioCreacion]) 
			VALUES (@ClienteId	, @Cui_Nit, @Direccion, @Fecha, @Total, @CajaId, @Usuario);
		END
		
		IF (@Opcion = 3) --Opci�n para actualizar
		BEGIN
			UPDATE FACTURA SET [ClienteId]=@ClienteId, [CUI_NIT]=@Cui_Nit, [Direccion]=@Direccion, [Fecha]=@Fecha, [Total]=@Total,
			 [CajaId] = @CajaId, [AuditUsuarioModificacion] = @Usuario
			WHERE [Id] = @Id;
		END
		
		IF (@Opcion = 4) --Opci�n para eliminar
		BEGIN
			DELETE FROM FACTURA WHERE [Id] = @Id; 
		END

		IF (@Opcion = 5) --Opci�n para seleccionar uno por ID
		BEGIN
			SELECT * FROM FACTURA WHERE [Id] = @Id;
		END

		COMMIT TRAN TRAN_MATENIMIENTO_FACTURA
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN TRAN_MATENIMIENTO_FACTURA
		SELECT ERROR_MESSAGE();
	END CATCH
END
GO
