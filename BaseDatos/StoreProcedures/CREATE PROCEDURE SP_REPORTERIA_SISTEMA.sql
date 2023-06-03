DROP PROCEDURE IF EXISTS [dbo].[SP_REPORTERIA_SISTEMA] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SP_REPORTERIA_SISTEMA
	@Opcion		INT,
	@Fecha		DATETIME
AS
BEGIN
	IF @Opcion = 1
	BEGIN
		SELECT CAJ.Id, PRO.Nombre, CAJ.efectivoApertura, CAJ.efectivoCierre, SUM(DET.SubTotal) AS Venta
		FROM FACTURA AS FAC
		INNER JOIN CAJA AS CAJ ON CAJ.Id = FAC.CajaId
		INNER JOIN DETALLE AS DET ON DET.FacturaId = FAC.Id
		INNER JOIN PRODUCTO AS PRO ON PRO.Id = DET.ProductoId
		WHERE FAC.Fecha = CONVERT(DATE, @Fecha)
		GROUP BY CAJ.Id, PRO.Nombre, CAJ.efectivoApertura, CAJ.efectivoCierre
		ORDER BY Venta DESC
	END

	IF @Opcion = 2
	BEGIN
		SELECT CAJ.Id, CAJ.efectivoApertura, CAJ.efectivoCierre, CAJ.AuditUsuarioCreacion, CAJ.AuditUsuarioModificacion
		FROM CAJA AS CAJ
		WHERE CAJ.AuditFechaCreacion = CONVERT(DATE, @Fecha)
	END

	IF @Opcion = 3
	BEGIN
		SELECT PROD.Nombre, PRO.Nombre, SUM(COM.Cantidad) Compra, 
		(SELECT SUM(DET.Cantidad) FROM DETALLE AS DET WHERE DET.ProductoId = COM.ProductoId) Venta, INV.Stock Inventario
		FROM COMPRA AS COM
		INNER JOIN PROVEEDOR AS PRO ON PRO.Id = COM.ProveedorId
		INNER JOIN INVENTARIO AS INV ON INV.ProductoId = COM.ProductoId
		INNER JOIN PRODUCTO AS PROD ON PROD.Id = COM.ProductoId
		WHERE COM.AuditFechaCreacion = CONVERT(DATE, @Fecha)
		GROUP BY PROD.Nombre, PRO.Nombre, COM.ProductoId, INV.Stock
	END
END
GO
