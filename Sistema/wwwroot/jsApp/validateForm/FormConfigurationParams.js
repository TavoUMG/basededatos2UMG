$(document).ready(function () {
    $('#FormConfigurationParams').validate({
        rules: {
            "sql.servidor_sql": { required: true, minlength: 4, maxlength: 75 },
            "sql.base_datos_sql": { required: true, minlength: 4, maxlength: 75 },
            "sql.usuario_sql": { required: true, minlength: 4, maxlength: 75 },
            "sql.contrasenia_sql": { required: true, minlength: 4, maxlength: 75 },
            "seg.servidor_sql": { required: true, minlength: 4, maxlength: 75 },
            "seg.base_datos_sql": { required: true, minlength: 4, maxlength: 75 },
            "seg.usuario_sql": { required: true, minlength: 4, maxlength: 75 },
            "seg.contrasenia_sql": { required: true, minlength: 4, maxlength: 75 },
            "app.es_administrador_app": { required: true },
            "avd.usuario_avd": { required: true, minlength: 4, maxlength: 75 },
            "avd.contrasenia_avd": { required: true, minlength: 4, maxlength: 75 },
            "avd.tiempo_respuesta_avd": { required: true, digits: true },
            "app.usuario_app": { required: true, minlength: 4, maxlength: 75 },
            "app.contrasenia_app": { required: true, minlength: 4, maxlength: 75 },
            "servidor.SERVIDOR": { required: true },
            "servidor.TIMER_ESTADO": { required: true, digits: true },
            "servidor.TIMER_ESTADO_INICIO": { required: true, digits: true },
            "servidor.TIMER_ERRORES": { required: true, digits: true },
            "servidor.TIMER_ERRORES_INICIO": { required: true, digits: true },
            "servidor.TIMER_HORARIO": { required: true, digits: true },
            "servidor.TIMER_HORARIO_INICIO": { required: true, digits: true },
            "servidor.TIMER_XML": { required: true, digits: true },
            "servidor.TIMER_XML_INICIO": { required: true, digits: true },
            "servidor.DIAS_PROCESO": { required: true, digits: true },
            "servidor.DIAS_PROCESO_NOCTURNO": { required: true, digits: true },
            "servidor.HORA_PROCESO_NOCTURNO": { required: true, digits: true },
            "servidor.GRUPO_PRCESAMIENTO": { required: true },
        },
        /*messages: {
            "sql.servidor_sql": {
                required: "El nombre o IP del servidor es obligatorio",
                minlength: "El nombre o IP del servidor debe de tener más de 3 caractéres",
                maxlength: "El nombre o IP del servidor debe de tener menos de 76 caractéres"
            },
            "sql.base_datos_sql": {
                required: "El nombre de la base de datos es obligatorio",
                minlength: "El nombre de la base de datos debe de tener más de 3 caractéres",
                maxlength: "El nombre de la base de datos debe de tener menos de 76 caractéres"
            },
            "sql.usuario_sql": {
                required: "El nombre del usuario de base de datos es obligatorio",
                minlength: "El nombre del usuario de base de datos debe de tener más de 3 caractéres",
                maxlength: "El nombre del usuario de base de datos debe de tener menos de 76 caractéres"
            },
            "sql.contrasenia_sql": {
                required: "La contraseña del usuario de base de datos es obligatoria",
                minlength: "La contraseña del usuario de base de datos debe de tener más de 3 caractéres",
                maxlength: "La contraseña del usuario de base de datos debe de tener menos de 76 caractéres"
            }
        },*/
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