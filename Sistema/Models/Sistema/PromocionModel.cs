namespace Sistema.Models.Sistema
{
    public enum OptionPromocion
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR_ID = 5
    }

    public class PromocionModel
    {
        public int Id { get; set; } = 0;
        public string Descripcion { get; set; }
        public int ProductoId { get; set; } = 0;
        public int TipoPromocionId { get; set; } = 0;
        public string ProductosId { get; internal set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();
        public ProductoModel Producto { get; set; } = new ProductoModel();
        public TipoPromocionModel TipoPromocion { get; set; } = new TipoPromocionModel();
    }
}
