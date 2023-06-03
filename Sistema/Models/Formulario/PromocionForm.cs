using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class PromocionForm
    {
        public int Id { get; set; } = 0;
        public string Descripcion { get; set; }
        public int ProductoId { get; set; } = 0;
        public int TipoPromocionId { get; set; } = 0;

        public List<PromocionModel> lista = new List<PromocionModel>();
        public List<TipoPromocionModel> promociones = new List<TipoPromocionModel>();
        public List<ProductoModel> productos = new List<ProductoModel>();
    }
}
