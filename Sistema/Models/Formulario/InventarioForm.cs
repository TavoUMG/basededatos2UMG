using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class InventarioForm
    {
        public int Id { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; } = 0;

        public List<InventarioModel> lista = new List<InventarioModel>();
        public List<ProductoModel> productos = new List<ProductoModel>();
    }
}
