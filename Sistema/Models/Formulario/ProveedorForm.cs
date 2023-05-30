using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class ProveedorForm
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string? Direccion { get; set; } = "";
        public string? Telefono { get; set; } = "";

        public List<ProveedorModel> lista = new List<ProveedorModel>();
    }
}
