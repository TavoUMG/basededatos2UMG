$(document).ready(function () {
    $('#FormReviewXML').validate({
        rules: {
            "clave": { required: true, minlength: 8, maxlength: 150 }
        },
        messages: {
            "clave": {
                required: "La clave es obligatoria",
                minlength: "La clave debe de tener más de 8 caractéres",
                maxlength: "La clave debe de tener menos de 35 caractéres"
            }
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