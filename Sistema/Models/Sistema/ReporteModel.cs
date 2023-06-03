namespace Sistema.Models.Sistema
{
    public enum OptionReporte
    {
        VENTA = 1,
        CAJA = 2,
        COMPRA = 3
    }

    public class ReporteVentaModel
    {
        public string Caja { get; set; }
        public string Producto { get; set; }
        public decimal Apertura { get; set; }
        public decimal Cierre { get; set; }
        public decimal Venta { get; set; }
    }

    public class ReporteCajaModel
    {
        public string Caja { get; set; }
        public decimal Apertura { get; set; }
        public decimal Cierre { get; set; }
        public string UsuarioAbre { get; set; }
        public string UsuarioCierra { get; set; }
    }

    public class ReporteCompraModel
    {
        public string Producto { get; set; }
        public string Proveedor { get; set; }
        public int Compra { get; set; }
        public int Venta { get; set; }
        public int Inventario { get; set; }
    }
}
