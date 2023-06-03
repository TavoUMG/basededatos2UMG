using Sistema.Class;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionProducto
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,        
        SELECCIONAR_ID = 5
    }
    public class ProductoModel
    {
        public int Id { get; set; } = 0;
        public int CategoriaId { get; set; } = 0;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 75 caracteres")]
        public string Nombre { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime Vencimiento { get; set; } = ClassUtilidad.fechaSistema();

        public string Imagen { get; set; } = "";

        public AuditoriaModel Auditoria = new AuditoriaModel();

        public CategoriaModel Categoria = new CategoriaModel();
    }
}
