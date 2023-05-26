using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Home
{
    public class CambiarPasswordModel
    {
        [Required(ErrorMessage = "El Código es obligatorio")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La Nueva Contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La Nueva Contraseña debe tener una longitud entre 6 y 50 caracteres")]
        public string NuevoPassword { get; set; }

        [Required(ErrorMessage = "La Confirmación de la Contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La Confirmación de la Contraseña debe tener una longitud entre 6 y 50 caracteres")]
        [Compare("NuevoPassword", ErrorMessage = "La Nueva Contraseña y su Confirmación deben ser iguales")]
        public string ConfirmarPassword { get; set; }
    }
}
