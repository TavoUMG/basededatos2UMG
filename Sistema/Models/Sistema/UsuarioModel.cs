﻿using Sistema.Class;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models.Sistema
{
    public enum OptionUsuario
    {
        TODOS = 1,
        CREAR = 2,
        EDITAR = 3,
        ELIMINAR = 4,
        SELECCIONAR = 5,
        INICIO_SESION = 6,
        NUEVO_PASSWORD = 7,
        SELECCIONAR_ID = 8
    }

    public class UsuarioModel
    {
        ClassSeguridad seguridad = new ClassSeguridad();

        private string password;

        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "El CUI es obligatorio")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "El CUI debe de contener 12 caracteres")]
        public string CUI { get; set; } = "";

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 75 caracteres")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "El Nombre debe de contener como mínimo 3 y un máximo de 75 caracteres")]
        public string Apellido { get; set; } = "";

        [Required(ErrorMessage = "La contraeña es obligatoria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe de contener como mínimo 6 y un máximo de 20 caracteres")]
        public string Password { get => password; set => password = seguridad.EncryptData(value); }

        public AuditoriaModel Auditoria = new AuditoriaModel();
    }
}
