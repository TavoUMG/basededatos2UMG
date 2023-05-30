using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class ProductoForm
    {
        public int Id { get; set; } = 0;
        public int CategoriaId { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public DateTime Vencimiento { get; set; }

        public List<ProductoModel> lista = new List<ProductoModel>();
        public List<CategoriaModel> categorias = new List<CategoriaModel>();
    }
}
