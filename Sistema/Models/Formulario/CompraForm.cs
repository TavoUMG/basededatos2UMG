using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class CompraForm
    {
        public int Id { get; set; } = 0;
        public int Cantidad { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public int ProveedorId { get; set; } = 0;
        public decimal PrecioCosto { get; set; }

        public List<CompraModel> lista = new List<CompraModel>();
        public List<ProductoModel> productos = new List<ProductoModel>();
        public List<ProveedorModel> proveedores = new List<ProveedorModel>();
    }
}
