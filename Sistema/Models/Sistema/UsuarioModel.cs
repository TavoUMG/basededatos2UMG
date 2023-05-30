using Sistema.Class;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionUsuario
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR = 5,
        INICIO_SESION = 6,
        NUEVO_PASSWORD = 7,
        SELECCIONAR_ID = 8
    }

    public class UsuarioModel
    {
        ClassSeguridad seguridad = new ClassSeguridad();

        private string password;

        public int Id { get; set; } = 0;

        public string CUI { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Password { get => password; set => password = seguridad.EncryptData(value); }

        public AuditoriaModel Auditoria = new AuditoriaModel();
    }
}
