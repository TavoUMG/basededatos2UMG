namespace Sistema.Models.Sistema
{
    public enum OptionCobro
    {
        TODOS = 1,
        CREAR = 2,
        ELIMINAR = 3,
        SELECCIONAR_ID = 4,
        FACTURA_PENDIENTE_PAGO = 5,
        MONTO_RESTANTE = 6
    }

    public class CobroModel
    {
        public int Id { get; set; } = 0;
        public decimal MontoReal { get; set; } = 0;
        public decimal Monto { get; set; } = 0;
        public decimal MontoRestante { get; set; } = 0;
        public int FacturaId { get; set; } = 0;
        public int UsuarioId { get; set; } = 0;

        public AuditoriaModel Auditoria = new AuditoriaModel();
        public FacturaModel Factura { get; set; } = new FacturaModel();
        public UsuarioModel Usuario { get; set; } = new UsuarioModel();
    }
}
