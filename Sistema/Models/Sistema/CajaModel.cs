using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionCaja
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        CAJA_ABIERTA = 5,
        CERRAR_CAJA = 6,
        SELECCIONAR_ID = 7
    }

    public class CajaModel
    {
        public int Id { get; set; } = 0;

        public int UsuarioId { get; set; } = 0;

        [Required(ErrorMessage = "La apertura de caja es obligatoria")]
        [Range(10, 2500, ErrorMessage = "Es necesario que la apertura de caja sea mayor a 9 y menor a 2501")]
        public decimal efectivoApertura { get; set; }

        [Required(ErrorMessage = "El cierre de caja es obligatoria")]
        public decimal efectivoCierre { get; set; }

        public AuditoriaModel Auditoria = new AuditoriaModel();

        public UsuarioModel Usuario = new UsuarioModel();
    }
}
