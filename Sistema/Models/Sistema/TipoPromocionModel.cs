namespace Sistema.Models.Sistema
{
    public enum OptionTipoPromocion
    {
        TODOS = 1,
        SELECCIONAR_ID = 2
    }

    public class TipoPromocionModel
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
    }
}
