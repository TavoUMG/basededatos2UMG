using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Sistema;
using Sistema.Models.Formulario;
using Sistema.Services;
using static Sistema.Models.View.ModelSweetAlert;
using Sistema.Util;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : NotificadorController
    {
        private readonly ILogger<UsuarioController> _logger;
        protected const string _controller = "UsuarioController";
        protected const string _dataTable = "DataTable/_UsuarioTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private UsuarioForm _form;

        public UsuarioController(ILogger<UsuarioController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new UsuarioForm();
        }

        // GET: Usuario/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.TODOS, new UsuarioModel {  }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Usuario/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(UsuarioForm form)
        {
            try
            {
                UsuarioModel model = new UsuarioModel
                {
                    Id = form.Id,
                    CUI = form.CUI,
                    Nombre = form.Nombre,
                    Apellido = form.Apellido,
                    Password = form.Password
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.TODOS, new UsuarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar el usuario: {ex.Message}" });
            }
        }

        // POST: Usuario/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(UsuarioForm form)
        {
            try
            {
                UsuarioModel model = new UsuarioModel
                {
                    Id = form.Id,
                    CUI = form.CUI,
                    Nombre = form.Nombre,
                    Apellido = form.Apellido,
                    Password = form.Password
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.TODOS, new UsuarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar el usuario: {ex.Message}" });
            }
        }

        // POST: Usuario/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.ELIMINAR, new UsuarioModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.TODOS, new UsuarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Eliminar");
                return BadRequest(new { error = $"Ocurrio un error al eliminar el usuario: {ex.Message}" });
            }
        }


        [HttpPost("Exists")]
        public IActionResult Exists(string CUI)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceUsuario(OptionUsuario.SELECCIONAR, new UsuarioModel { CUI = CUI }, _usuario);

                return Json(data: _form.lista.Count == 0);
            }
            catch
            {
                return Json(data: false);
            }
        }
    }
}
