DROP TRIGGER IF EXISTS [DBO].[PAGADO_AFTER_INSERT_FACTURA_COBRO];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [DBO].[PAGADO_AFTER_INSERT_FACTURA_COBRO] 
   ON  [DBO].[COBRO] 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @MontoResta DECIMAL(20,2) = (SELECT I.MontoRestante FROM Inserted AS I);

	IF @MontoResta = 0
	BEGIN
		UPDATE [DBO].[FACTURA]
		SET [Pagado] = 1
		WHERE Id = (SELECT I.FacturaId FROM Inserted AS I);
	END
END
GO
