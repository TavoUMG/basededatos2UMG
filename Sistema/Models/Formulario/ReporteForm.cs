using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class ReporteForm
    {
        public DateTime Fecha { get; set; } = Class.ClassUtilidad.fechaSistema().Date;

        public List<ReporteVentaModel> Ventas = new List<ReporteVentaModel>();
        public List<ReporteCajaModel> Cajas = new List<ReporteCajaModel>();
        public List<ReporteCompraModel> Compras = new List<ReporteCompraModel>();
    }
}
