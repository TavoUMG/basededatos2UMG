using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionDevolucion
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5
    }
    public class DevolucionModel
    {
        public int Id { get; set; } = 0;
        public int Cantidad { get; set; } = 0;
        public int FacturaId { get; set; } = 0;
        public int DetalleId { get; set; } = 0;
        public int ProductoId { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

        public FacturaModel Factura = new FacturaModel();

        public DetalleModel Detalle = new DetalleModel();

        public ProductoModel Producto = new ProductoModel();
    }
}
