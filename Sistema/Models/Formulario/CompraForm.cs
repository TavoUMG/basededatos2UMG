using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class CompraForm
    {
        public CompraModel model = new CompraModel();
        public List<CompraModel> lista = new List<CompraModel>();
        public List<ProductoModel> productos = new List<ProductoModel>();
        public List<ProveedorModel> proveedores = new List<ProveedorModel>();
    }
}
