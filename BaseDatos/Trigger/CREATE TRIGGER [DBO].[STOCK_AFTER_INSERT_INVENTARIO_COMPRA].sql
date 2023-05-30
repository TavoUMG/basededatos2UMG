DROP TRIGGER IF EXISTS [DBO].[STOCK_AFTER_INSERT_INVENTARIO_COMPRA];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [DBO].[STOCK_AFTER_INSERT_INVENTARIO_COMPRA] 
   ON  [DBO].[COMPRA] 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	IF ((SELECT COUNT(*) FROM [DBO].[INVENTARIO] AS INV WHERE INV.ProductoId = (SELECT I.ProductoId FROM Inserted AS I)) > 0)
	BEGIN
		UPDATE [DBO].[INVENTARIO]
		SET [Stock] = [Stock] + (SELECT I.Cantidad FROM Inserted AS I),
		[AuditUsuarioModificacion] = (SELECT I.AuditUsuarioCreacion FROM Inserted AS I)
		WHERE ProductoId = (SELECT I.ProductoId FROM Inserted AS I);
	END
	ELSE
	BEGIN
		INSERT INTO [DBO].[INVENTARIO]
				([ProductoId], [PrecioVenta], [Stock], [AuditUsuarioCreacion])
		VALUES	((SELECT I.ProductoId FROM Inserted AS I), (SELECT ((I.PrecioCosto*35)/100)+(I.PrecioCosto) FROM Inserted AS I), (SELECT I.Cantidad FROM Inserted AS I), (SELECT I.AuditUsuarioCreacion FROM Inserted AS I));
	END
END
GO
