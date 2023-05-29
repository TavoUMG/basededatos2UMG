using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Sistema;
using Sistema.Services;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : NotificadorController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        protected const string _controller = "UsuarioController";
        private ServiceSQLServer _service;
        private String _usuario;

        public UsuarioController(ILogger<UsuarioController> logger, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _service = new ServiceSQLServer();
            _usuario = httpContextAccessor.HttpContext.Session.GetString("userName");
        }

        // GET: Usuario/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                List<UsuarioModel> data = new List<UsuarioModel> { new UsuarioModel { Id = 0} };
                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.TODOS, data[0], _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // GET: Usuario/Insertar
        [HttpGet("Insertar")]
        public ActionResult Insertar()
        {
            try
            {
                DateTime fecha = DateTime.Now;
                List<UsuarioModel> data = new List<UsuarioModel> 
                {   
                    new UsuarioModel { 
                        Id = 0,
                        CUI = $"{fecha.ToString("dd/MM/yyyy/HH/ss").Replace("/","")}",
                        Nombre = "Héctor",
                        Apellido = "de la Cruz",
                        Password = "Admin123"
                    }
                };

                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.CREAR, data[0], _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return RedirectToAction(actionName: "Index", controllerName: "Usuario");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                AlertSuperior($"Ha ocurrido un error al cargar la pantalla: {ex.Message}", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // GET: Usuario/Actualizar
        [HttpGet("Actualizar")]
        public ActionResult Actualizar()
        {
            try
            {
                DateTime fecha = DateTime.Now;
                List<UsuarioModel> data = new List<UsuarioModel>
                {
                    new UsuarioModel {
                        Id = 2,
                        CUI = $"{fecha.ToString("dd/MM/yyyy/HH/ss").Replace("/","")}",
                        Nombre = "Héctor",
                        Apellido = "de la Cruz",
                        Password = "Admin123"
                    }
                };

                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.EDITAR, data[0], _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return RedirectToAction(actionName: "Index", controllerName: "Usuario");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                AlertSuperior($"Ha ocurrido un error al cargar la pantalla: {ex.Message}", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // GET: Usuario/Eliminar
        [HttpGet("Eliminar/{id}")]
        public ActionResult Eliminar(int id)
        {
            try
            {
                List<UsuarioModel> data = new List<UsuarioModel>
                {
                    new UsuarioModel {
                        Id = id
                    }
                };

                (bool respuesta, string mensaje, data) = _service.ServiceUsuario(OptionUsuario.ELIMINAR, data[0], _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return RedirectToAction(actionName: "Index", controllerName: "Usuario");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                AlertSuperior($"Ha ocurrido un error al cargar la pantalla: {ex.Message}", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
