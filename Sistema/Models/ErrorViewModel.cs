namespace Sistema.Models
{
    public enum TypeError
    {
        ERROR400 = 0,
        ERROR401 = 1,
        ERROR403 = 2,
        ERROR404 = 3,
        ERROR408 = 4,
        ERROR500 = 5
    }

    public static class MeaningTypeError
    {
        public static (string, string, int) Meaning(TypeError type)
        {
            string[] title = {
                "Bad request",
                "Unauthorized",
                "Forbidden",
                "Not found",
                "Request timeout",
                "Server error"
            };

            string[] error = { 
                "El servidor no puede responder debido a un error con el cliente.",
                "El cliente debe autentificarse para obtener una respuesta.",
                "El cliente no tiene permiso para acceder al contenido.",
                "Un error muy común. No se reconoce la URL; la fuente no existe.",
                "Al servidor le gustaría cerrar una conexión inactiva, pero la solicitud no se ha completado antes del tiempo de espera.",
                "La solicitud ha sido aceptada, pero debido a un error con el servidor, no se ha podido completar la petición."
            };

            int[] code = { 
                400, 401, 403, 404, 408, 500
            };

            return (title[(int)type], error[(int)type], code[(int)type]);
        }
    }

    [Serializable]
    public class ErrorPersonalizedViewModel
    {
        public string title { get; set; }
        public string error { get; set; }
        public int code { get; set; }
        public string description { get; set; }
        public string? returnUrl { get; set; }

        public ErrorPersonalizedViewModel(TypeError type)
        {
            (string title, string error, int code) = MeaningTypeError.Meaning(type);

            this.title = title;
            this.error = error;
            this.code = code;
        }

        public ErrorPersonalizedViewModel()
        {

        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}