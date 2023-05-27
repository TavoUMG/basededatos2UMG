using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionProveedor
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5
    }
    public class ProveedorModel
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 75 caracteres")]
        public string Nombre { get; set; } = "";

        [StringLength(75, MinimumLength = 10, ErrorMessage = "La Dirección debe de contener como mínimo 10 y un máximo de 75 caracteres")]
        public string? Direccion { get; set; } = "";


        [StringLength(25, MinimumLength = 13, ErrorMessage = "El Telefono debe de contener como mínimo 13 y un máximo de 25 caracteres")]
        public string? Telefono { get; set; } = "";
        
        public AuditoriaModel Auditoria = new AuditoriaModel();

    }
}
