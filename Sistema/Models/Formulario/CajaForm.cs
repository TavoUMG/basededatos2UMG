using Sistema.Models.Sistema;

namespace Sistema.Models.Formulario
{
    public class CajaForm
    {
        public int Id { get; set; } = 0;
        public int UsuarioId { get; set; } = 0;
        public decimal efectivoApertura { get; set; }
        public decimal efectivoCierre { get; set; }

        public List<CajaModel> lista = new List<CajaModel>();
    }
}
