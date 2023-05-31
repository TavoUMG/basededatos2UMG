using Sistema.Class;
using Sistema.Models.Sistema;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Formulario
{
    public class FacturaForm
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "El Cliente es obligatorio")]
        [Range(1, 100000, ErrorMessage = "El Cliente seleccionado no es correcto.")]
        public int ClienteId { get; set; } = 0;

        [Required(ErrorMessage = "El CUI o NIT es obligatorio")]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "El CUI o NIT debe de contener entre 6 y 15 caracteres")]
        public string CUI_NIT { get; set; } = ""; 
        
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El CUI o NIT debe de contener entre 3 y 100 caracteres")]
        public string? Direccion { get; set; } = "";

        [Required(ErrorMessage = "La Fecha es obligatoria")]
        public DateTime Fecha { get; set; } = ClassUtilidad.fechaSistema().Date;

        [Required(ErrorMessage = "El Total es obligatorio")]
        public decimal Total { get; set; }

        //Sección Form Detalle
        public int? ProductoId { get; set; } = 0;
        public int? Cantidad { get; set; } = 0;

        public List<DatelleForm> Detalle { get; set; }

        public List<FacturaModel> lista { get; set; } = new List<FacturaModel>();
        public List<ClienteModel> clientes = new List<ClienteModel>();
        public List<InventarioModel> productos = new List<InventarioModel>();
    }
}
