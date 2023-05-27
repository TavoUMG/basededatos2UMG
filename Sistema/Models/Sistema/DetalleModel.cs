using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionDetalle
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5
    }
    public class DetalleModel
    {
        public int Id { get; set; } = 0;
        public int FacturaId { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public int Cantidad { get; set; } = 0;

        [Required(ErrorMessage = "El Precio es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El Sub Total es obligatorio")]
        [DataType(DataType.Currency)]
        public decimal SubTotal { get; set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();
    }
}
