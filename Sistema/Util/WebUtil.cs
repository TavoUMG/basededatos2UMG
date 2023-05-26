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

        public static (string, int?) GetServiceValues(ISession session)
        {
            string token = session.GetString("userToken");
            int? idLogin = session.GetString("userId") != null ? int.Parse(session.GetString("userId")) : null;

            return (token, idLogin);
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
