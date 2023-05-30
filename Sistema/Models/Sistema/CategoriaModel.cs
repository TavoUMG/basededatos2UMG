using Sistema.Class;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionCategoria
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,        
        SELECCIONAR_ID = 5
    } 
    public class CategoriaModel
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 75 caracteres")]
        public string Nombre { get; set; } = "";

        public AuditoriaModel Auditoria = new AuditoriaModel();
        
    }
}
