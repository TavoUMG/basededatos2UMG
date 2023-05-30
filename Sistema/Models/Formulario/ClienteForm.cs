using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class ClienteForm
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string? Telefono { get; set; } = "";
        public string? Direccion { get; set; } = "";

        public List<ClienteModel> lista = new List<ClienteModel>();
    }
}
