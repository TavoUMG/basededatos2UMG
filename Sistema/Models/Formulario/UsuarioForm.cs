using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class UsuarioForm
    {
        public int Id { get; set; } = 0;
        public string CUI { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Password { get; set; } = "";

        public List<UsuarioModel> lista = new List<UsuarioModel>();
    }
}
