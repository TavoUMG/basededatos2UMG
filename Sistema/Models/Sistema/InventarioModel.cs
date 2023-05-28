namespace Sistema.Models.Sistema
{
    public enum OptionInventario
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5
    }
    public class InventarioModel
    {
        public int Id { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; } = 0;

        public AuditoriaModel Auditoria = new AuditoriaModel();

    }
}
