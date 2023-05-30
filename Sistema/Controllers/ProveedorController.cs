using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class ProveedorController : NotificadorController
    {
        private readonly ILogger<ProveedorController> _logger;
        protected const string _controller = "ProveedorController";
        protected const string _dataTable = "DataTable/_ProveedorTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private ProveedorForm _form;

        public ProveedorController(ILogger<ProveedorController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new ProveedorForm();
        }

        // GET: Prvoeedor/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.TODOS, new Models.Sistema.ProveedorModel { }, _usuario);
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

        // POST: Prvoeedor/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(ProveedorForm form)
        {
            try
            {
                Models.Sistema.ProveedorModel model = new Models.Sistema.ProveedorModel 
                { 
                    Id = form.Id,
                    Nombre = form.Nombre,
                    Direccion = form.Direccion,
                    Telefono = form.Telefono,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.TODOS, new Models.Sistema.ProveedorModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Prvoeedor/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ProveedorForm form)
        {
            try
            {
                Models.Sistema.ProveedorModel model = new Models.Sistema.ProveedorModel
                {
                    Id = form.Id,
                    Nombre = form.Nombre,
                    Direccion = form.Direccion,
                    Telefono = form.Telefono,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.TODOS, new Models.Sistema.ProveedorModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Prvoeedor/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.ELIMINAR, new Models.Sistema.ProveedorModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.TODOS, new Models.Sistema.ProveedorModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Eliminar");
                return BadRequest(new { error = $"Ocurrio un error al eliminar: {ex.Message}" });
            }
        }
    }
}
