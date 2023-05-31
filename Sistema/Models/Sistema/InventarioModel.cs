namespace Sistema.Models.Sistema
{
    public enum OptionInventario
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5,
        PRODUCTO_ID = 6
    }
    public class InventarioModel
    {
        public int Id { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; } = 0;
        public bool Activo { get; set; } = false;
        public string ValorSelect { get; internal set; }
        public string ValorFlied { get; internal set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

        public ProductoModel Producto = new ProductoModel();
    }
}
