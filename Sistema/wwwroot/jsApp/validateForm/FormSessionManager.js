$(document).ready(function () {
    $('#FormSessionManager').validate({
        rules: {
            "usuario_app": { required: true, minlength: 4, maxlength: 75 },
            "contrasenia_app": { required: true, minlength: 4, maxlength: 75 }
        },
        messages: {
            "usuario_app": {
                required: "El usuario administrador es obligatorio",
                minlength: "El usuario administrador debe de tener más de 3 caractéres",
                maxlength: "El usuario administrador debe de tener menos de 25 caractéres"
            },
            "contrasenia_app": {
                required: "La contraseña del usuario administrador es obligatoria",
                minlength: "La contraseña del usuario administrador debe de tener más de 3 caractéres",
                maxlength: "La contraseña del usuario administrador debe de tener menos de 25 caractéres"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        invalidHandler: function (event, validator) {
            var errors = validator.numberOfInvalids();

            if (errors) {
                var message = errors == 1
                    ? `Al paracer el sistema detecto que ${errors} campo no cumple con la validación.`
                    : `Al paracer el sistema detecto que ${errors} campos no cumplen con la validación.`;

                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: message,
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        }

    });
});