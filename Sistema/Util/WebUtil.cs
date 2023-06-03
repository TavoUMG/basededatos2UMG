using IronBarCode;
using Sistema.Class;
using System.Net;
using System.Net.Mail;

namespace Sistema.Util
{
    public static class WebUtil
    {
        public static void AddCookie(HttpContext context, string value)
        {
            ClassSeguridad seg = new ClassSeguridad();

            var cookieOptions = new CookieOptions();
            cookieOptions.Secure = true;
            cookieOptions.HttpOnly = true;
            cookieOptions.IsEssential = false;
            cookieOptions.MaxAge = TimeSpan.MaxValue;
            context.Response.Cookies.Append("SiteToken", seg.EncryptData(value), cookieOptions);
        }

        public static (string CUI, string NombreCompleto, int ID) GetServiceValues(ISession session)
        {
            string token = session.GetString("userToken");
            string nombre = session.GetString("userName");
            int idLogin = ClassUtilidad.parseMultiple(session.GetString("userId"), ClassUtilidad.TipoDato.Integer).numero;

            return (token, nombre, idLogin);
        }

        public static string generarQR(string webRootPath, string factura, string data)
        {
            string dirPath = Path.Combine(webRootPath, "QR");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string saveToPath = Path.Combine(dirPath, $"qr_{factura}.png");

            QRCodeWriter.CreateQrCode(data, 250, QRCodeWriter.QrErrorCorrectionLevel.Medium).AddBarcodeValueTextAboveBarcode(TextSpacing: 1).SaveAsPng(saveToPath);

            return saveToPath;
        }

        public static bool generarPDF(string _hostEnvironment, Models.Sistema.FacturaModel factura, List<Models.Sistema.DetalleModel> detalle, string host)
        {
            TicketPDF ticket = new TicketPDF();
            ticket.Path = Path.Combine(_hostEnvironment, "Tickets", factura.Archivo);
            ticket.HeaderImage = Path.Combine(_hostEnvironment, "images", "logo.png");

            ticket.AddHeaderLine("NIT: 1002827029");
            ticket.AddHeaderLine($"FACTURA Nro.: {factura.Numero}");
            ticket.AddHeaderLine($"AUTORIZACION Nro.: {ClassUtilidad.fechaSistema().ToString("fff/ss/HH/mm/MM/yyyy").Replace("/", "")}");

            ticket.AddSubHeaderLine($"Fecha: {factura.Fecha.ToString("dd/mm/yyyy")}");
            ticket.AddSubHeaderLine($"NIT/CUI: {factura.CUI_NIT}");
            ticket.AddSubHeaderLine($"Cliente: {factura.Cliente.NombreCompleto}");

            string ids = String.Empty;
            for (int i = 0; i < detalle.Count; i++)
            {
                ids = i == 0 ? detalle[i].ProductoId.ToString() : $"{ids},{detalle[i].ProductoId}";

                ticket.AddItem(
                    cantidad: detalle[i].Cantidad.ToString(),
                    item: detalle[i].Producto.Nombre,
                    price: String.Format("{0:0,0.00}", detalle[i].Precio),
                    total: String.Format("{0:0,0.00}", detalle[i].SubTotal)
                );
            }

            //El metodo AddTotal requiere 2 parametros, 
            //la descripcion del total, y el precio 
            ticket.AddTotal("TOTAL", String.Format("{0:0,0.00}", factura.Total));

            //El metodo AddFooterLine funciona igual que la cabecera 
            ticket.AddFooterLine($"Son: {ConversoresNumeroLetras.Parse(factura.Total)}");
            ticket.AddFooterLine($"\n");
            ticket.AddFooterLine($"Código de Control: {ClassUtilidad.GUID()}");
            ticket.AddFooterLine($"Fecha Límite de Emisión: {factura.Fecha.AddMonths(2).Date.ToString("dd/MM/yyyy")}");

            ClassSeguridad seguridad = new ClassSeguridad();
            ticket.FooterQR = WebUtil.generarQR(_hostEnvironment, factura.Numero, $"{host}/Marcketing/{seguridad.EncryptData($"{factura.Id}-{factura.Numero}")}/Promociones/{seguridad.EncryptData(ids)}");

            return ticket.Print();
        }

        public static (bool respuesta, string mensaje) guardarImagen(string path, IFormFile file, bool validar = true)
        {
            (bool respuesta, string mensaje) = (false, "");

            try
            {
                if(!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (validar && file == null) throw new Exception("La imagen es obligatoria");

                mensaje = $"{ClassUtilidad.GUID()}{Path.GetExtension(file.FileName)}";

                using (var fileStream = new FileStream(Path.Combine(path, mensaje), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                respuesta = true;
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = $"Error al guardar imagen: {ex.Message}";
            }

            return (respuesta, mensaje);
        }

        public static void SendEmail(string to, string name, string subject, string body, string password)
        {
            try
            {
                var createMessageId = bool.Parse(Environment.GetEnvironmentVariable("MailSmtpCreateMessageId"));
                var email = new MailMessage();
                email.From = new MailAddress(Environment.GetEnvironmentVariable("MailSmtpUserName"), Environment.GetEnvironmentVariable("MailSmtpFromName"));
                email.To.Add(new MailAddress(to, name));

                email.Subject = subject;
                email.IsBodyHtml = true;
                email.Body = body;

                if (createMessageId)
                {
                    email.Headers.Add("Message-Id",
                         String.Format("<{0}@{1}>",
                         Guid.NewGuid().ToString(),
                         Environment.GetEnvironmentVariable("MailSmtpHost")));
                }

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = Environment.GetEnvironmentVariable("MailSmtpHost");
                    smtp.Port = int.Parse(Environment.GetEnvironmentVariable("MailSmtpPort"));
                    smtp.EnableSsl = bool.Parse(Environment.GetEnvironmentVariable("MailSmtpUseSSL"));
                    smtp.Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("MailSmtpUserName"), password);

                    smtp.Send(email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error enviando el correo", ex);
            }
        }

        public static string CreateBodyRecoverPassword (string templatePath, string name, string code, string link)
        {
            try
            {
                var body = "";
                using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
                {
                    body = SourceReader.ReadToEnd();
                }
                body = body.Replace("<<nombre>>", name);
                body = body.Replace("<<codigo>>", code);
                body = body.Replace("<<link>>", link);

                return body;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error creando el contenido del correo de recuperacion de contrase�a", ex);
            }
        }
    }
}
