using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Home
{
    public class RecuperarPasswordModel
    {
        [Required(ErrorMessage = "El CUI es obligatorio")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "El CUI debe de contener 12 caracteres")]
        public string CUI { get; set; }

        [Required(ErrorMessage = "El Correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo no tiene el formato correcto")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
