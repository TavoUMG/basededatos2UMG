using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionCompra
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,        
        SELECCIONAR_ID = 5
    }

    public class CompraModel
    {
        public int Id { get; set; } = 0;
        public int Cantidad { get; set; } = 0;
        public int ProductoId { get; set; } = 0;
        public int ProveedorId { get; set; } = 0;
        public int CajaId { get; set; } = 0;

        [Required(ErrorMessage = "El Precio Costo es obligatoria")]
        public decimal PrecioCosto { get; set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

        public ProductoModel Producto = new ProductoModel();

        public ProveedorModel Proveedor = new ProveedorModel();
    }
}
