$(document).ready(function () {
    $('#FormConsultRF').validate({
        rules: {
            "serie": { minlength: 3, maxlength: 15 },
            "numero": { minlength: 1, maxlength: 15, digits: true },
            "consecutivo": { minlength: 1, maxlength: 50, digits: true },
        },
        messages: {
            "serie": {
                minlength: "La serie debe de tener más de 2 caractéres",
                maxlength: "La serie debe de tener menos de 16 caractéres"
            },
            "numero": {
                minlength: "El número debe de tener más de 0 caractéres",
                maxlength: "El número debe de tener menos de 16 caractéres",
                digits: "El número solo permite digitos"
            },
            "consecutivo": {
                minlength: "El consecutivo debe de tener más de 0 caractéres",
                maxlength: "El consecutivo debe de tener menos de 51 caractéres",
                digits: "El consecutivo solo permite digitos"
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