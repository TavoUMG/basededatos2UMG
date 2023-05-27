using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionFactura
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,        
        SELECCIONAR_ID = 5
    }
    public class FacturaModel
    {
        public int Id { get; set; } = 0;
        public int ClienteId { get; set; } = 0;

        [Required(ErrorMessage = "El CUI o NIT es obligatorio")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "El CUI debe de contener 15 caracteres")]
        public string CUI_NIT { get; set; } = "";

        [StringLength(250, MinimumLength = 10, ErrorMessage = "La Dirección debe de contener como mínimo 10 y un máximo de 250 caracteres")]
        public string? Direccion { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El Total es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

    }
}
