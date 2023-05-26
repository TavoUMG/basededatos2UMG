using Sistema.Models;
using Sistema.Models.Home;
using Sistema.Services;
using Sistema.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using static Sistema.Models.View.ModelSweetAlert;
using Sistema.Class;
using Sistema.Models.Sistema;

namespace Sistema.Controllers
{
    public class HomeController : NotificadorController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        protected const string _controller = "HomeController";
        private ServiceSQLServer _service;
        private ClassSeguridad _seguridad;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _service = new ServiceSQLServer();
            _seguridad = new ClassSeguridad();
        }

        #region Index
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userToken") == null)
                return RedirectToAction($"Login", "Home");
            else
                return View();
        }
        #endregion

        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("userToken") != null)
                return RedirectToAction($"Index", "Home");

            //Se llama a borrar la cookie para el caso que no exista la sesion y la cookie siga existiendo
            DeleteCookie();

            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection user)
        {
            try
            {
                var username = user["username"];
                var password = user["password"];

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    AlertSuperior("El usuario y la contraseña son requeridos", NotificationType.error);
                    return View();
                }

                var encriptado = _seguridad.EncryptData(password);
                List<UsuarioModel> data = new List<UsuarioModel> { new UsuarioModel { CUI = username, Password = password } };
                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.INICIO_SESION, data[0]);
                if (!respuesta) throw new Exception(mensaje);

                HttpContext.Session.SetString("userName", $"{data[0].Nombre} {data[0].Apellido}");
                HttpContext.Session.SetString("userId", data[0].Id.ToString());
                HttpContext.Session.SetString("userToken", data[0].CUI);

                //Se agrega la cookie sin fecha de expiracion
                WebUtil.AddCookie(HttpContext, data[0].CUI);

                return RedirectToAction($"Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Login");
                AlertSuperior(ex.Message, NotificationType.error);
                return View();
            }
        }
        #endregion

        #region Logout
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            //Se limpia la sesion y se elimina el cookie
            HttpContext.Session.Clear();
            DeleteCookie();
            return RedirectToAction($"Index", "Home");
        }
        #endregion

        #region RecuperarPassword
        [HttpGet]
        [AllowAnonymous]
        [Route("/Home/RecuperarPassword", Name = "Auth_RecuperarPassword")]
        public IActionResult RecuperarPassword()
        {
            var model = new RecuperarPasswordModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Home/RecuperarPassword", Name = "Auth_RecuperarPasswordPost")]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarPassword(RecuperarPasswordModel model)
        {
            try
            {
                List<UsuarioModel> data = new List<UsuarioModel> { new UsuarioModel { CUI = model.CUI } };
                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.SELECCIONAR, data[0]);
                if (!respuesta) throw new Exception(mensaje);

                var key = _seguridad.EncryptData($"{data[0].Id}|{ClassUtilidad.fechaSistema().AddMinutes(60).ToString("dd/MM/yyyy HH:mm:ss")}");
                var code = _seguridad.GenerateToken();
                var templatePath = $"{_hostingEnvironment.WebRootPath}{Path.DirectorySeparatorChar}EmailTemplates{Path.DirectorySeparatorChar}PlantillaRecuperarPassword.html";
                var link = String.Format(Environment.GetEnvironmentVariable("RecoverPasswordURL"), key.Replace($"/", "%2F"), _seguridad.EncryptData(code).Replace($"/", "%2F"));
                var body = WebUtil.CreateBodyRecoverPassword(templatePath, $"{data[0].Nombre} {data[0].Apellido}", code, link);
                var password = Environment.GetEnvironmentVariable("MailSmtpPassword");

                WebUtil.SendEmail(model.Email, $"{data[0].Nombre} {data[0].Apellido}", "Recuperación de contraseña", body, password);

                return RedirectToAction($"Login", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - ReestablecerPassword");
                AlertSuperior(ex.Message, NotificationType.error);
                return View();
            }
        }
        #endregion

        #region Cambiar Password
        [HttpGet]
        [AllowAnonymous]
        [Route("/Home/CambiarPassword/{key}/{code}", Name = "Auth_CambiarPassword")]
        public IActionResult CambiarPassword(string key, string code)
        {
            try
            {
                CambiarPasswordModel user = new CambiarPasswordModel();
                var keyDecrypt = _seguridad.DecryptData(key.Replace("%2F", $"/"));
                var dateText = keyDecrypt.Split("|")[1];
                var date = DateTime.ParseExact(dateText, "dd/MM/yyyy HH:mm:ss", null);

                if (ClassUtilidad.fechaSistema() > date)
                {
                    AlertSuperior("El tiempo de vida del enlace de cambio de contraseña se ha vencido.  Favor solicitar uno nuevo.", NotificationType.error);
                    return RedirectToAction($"Login", "Home");
                }

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - CambiarPassword");
                AlertSuperior("Ha ocurrido un error con el link de cambio de contraseña", NotificationType.error);
                return RedirectToAction($"Login", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Home/CambiarPassword/{key}/{code}", Name = "Auth_CambiarPasswordPost")]
        [ValidateAntiForgeryToken]
        public IActionResult CambiarPassword(string key, string code, CambiarPasswordModel user)
        {
            try
            {
                string keyDecrypt = _seguridad.DecryptData(key.Replace("%2F", $"/"));
                int userId = ClassUtilidad.parseMultiple(keyDecrypt.Split("|")[0], ClassUtilidad.TipoDato.Integer).numero;
                string codeDecrypt = _seguridad.DecryptData(code.Replace("%2F", $"/"));
                
                if (codeDecrypt != user.Codigo)
                {
                    AlertSuperior("El código ingresado no coincide con el link proporcionado.", NotificationType.error);
                    return View(user);
                }

                List<UsuarioModel> data = new List<UsuarioModel> { new UsuarioModel { Id = userId, Password = user.NuevoPassword } };
                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.NUEVO_PASSWORD, data[0]);
                if (!respuesta) throw new Exception(mensaje);

                return RedirectToAction($"Login", "Home");
            }
            catch (Exception ex)
            {
                user = new CambiarPasswordModel();
                _logger.LogWarning(exception: ex, message: $"{_controller}  - CambiarPassword");
                AlertSuperior(ex.Message, NotificationType.error);
                return View(user);
            }
        }
        #endregion

        public IActionResult Framework()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Exception/Screen", Name = "Exception")]
        public IActionResult Exception(string title, string error, int code, string description)
        {
            var model = new ErrorPersonalizedViewModel();
            model.code = code;
            model.description = description;
            model.error = error;
            model.title = title;

            model.returnUrl = Environment.GetEnvironmentVariable("ReturnUrl");

            return View(model);
        }

        #region Funciones
        private void DeleteCookie()
        {
            try
            {
                //Si no existe sesion pero se ha quedado la cookie debido a que no se le dio cerrar sesion,
                //se elimina la cookie y se ejecuta la accion de logout del api
                if (HttpContext.Session.GetString("userToken") == null && HttpContext.Request.Cookies["SiteToken"] != null)
                {
                    var seg = new ClassSeguridad();
                    var token = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key.Equals("SiteToken"));
                    HttpContext.Response.Cookies.Delete("SiteToken");
                }
            }
            catch(Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - DeleteCookie");
            }
        }
        #endregion
    }
}