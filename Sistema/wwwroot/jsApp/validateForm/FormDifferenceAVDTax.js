$(document).ready(function () {
    $('#FormDifferenceAVDTax').validate({
        rules: {
            "start": { required: true },
            "end": { required: true },
            "group": { required: true, digits: true }
        },
        messages: {
            "start": {
                required: "La fecha inicial es obligatoria"
            },
            "end": {
                required: "La fecha final es obligatoria"
            },
            "group": {
                required: "El local es obligatorio",
                digits: "El local solo acepta números"
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