using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class CobroForm
    {
        public int Id { get; set; } = 0;
        public decimal Monto { get; set; } = 0;
        public int FacturaId { get; set; } = 0;
        public int UsuarioId { get; set; } = 0;

        public List<CobroModel> lista = new List<CobroModel>();
        public List<FacturaModel> facturas = new List<FacturaModel>();
    }
}
