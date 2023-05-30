using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class CategoriaForm
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";

        public List<CategoriaModel> lista = new List<CategoriaModel>();
    }
}
