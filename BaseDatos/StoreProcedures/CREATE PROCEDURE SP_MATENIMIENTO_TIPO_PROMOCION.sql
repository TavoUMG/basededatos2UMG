DROP PROCEDURE IF EXISTS [dbo].[SP_MATENIMIENTO_TIPO_PROMOCION] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_MATENIMIENTO_TIPO_PROMOCION
AS
BEGIN
	SELECT * FROM TIPO_PROMOCION;
END
GO
