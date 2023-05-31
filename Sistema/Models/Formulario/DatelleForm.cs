namespace Sistema.Models.Formulario
{
    public class DatelleForm
    {
        public string Producto { get; set; } = "";
        public int ProductoId { get; set; } = 0;
        public int Cantidad { get; set; } = 0;
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
    }
}
