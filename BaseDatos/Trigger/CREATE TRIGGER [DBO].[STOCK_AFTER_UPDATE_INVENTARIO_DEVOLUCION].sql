DROP TRIGGER IF EXISTS [DBO].[STOCK_AFTER_UPDATE_INVENTARIO_DEVOLUCION];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [DBO].[STOCK_AFTER_UPDATE_INVENTARIO_DEVOLUCION] 
   ON  [DBO].[DEVOLUCION] 
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Cantidad INT = ((SELECT I.Cantidad FROM Inserted AS I) - (SELECT D.Cantidad FROM Deleted AS D));

    -- Insert statements for trigger here
	UPDATE [DBO].[INVENTARIO]
	SET [Stock] = [Stock] + @Cantidad,
	[AuditUsuarioModificacion] = (SELECT I.AuditUsuarioModificacion FROM Inserted AS I)
	WHERE ProductoId = (SELECT I.ProductoId FROM Inserted AS I);
END
GO
