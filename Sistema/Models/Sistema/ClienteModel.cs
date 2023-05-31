using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionCliente
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,        
        SELECCIONAR_ID = 5
    }
    public class ClienteModel
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 50 caracteres")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Apellido debe de contener como mínimo 3 y un máximo de 50 caracteres")]
        public string Apellido { get; set; } = "";

        
        [StringLength(20, MinimumLength = 13, ErrorMessage = "El Telefono debe de contener como mínimo 13 y un máximo de 20 caracteres")]
        public string? Telefono { get; set; } = "";

        
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La Dirección debe de contener como mínimo 10 y un máximo de 500 caracteres")]
        public string? Direccion { get; set; } = "";
        public string NombreCompleto { get; internal set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

    }
}
