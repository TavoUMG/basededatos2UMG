using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class ClienteController : NotificadorController
    {
        private readonly ILogger<ClienteController> _logger;
        protected const string _controller = "ClienteController";
        protected const string _dataTable = "DataTable/_ClienteTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private ClienteForm _form;

        public ClienteController(ILogger<ClienteController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new ClienteForm();
        }

        // GET: Cliente/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario);
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

        // POST: Cliente/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(ClienteForm form)
        {
            try
            {
                Models.Sistema.ClienteModel model = new Models.Sistema.ClienteModel 
                { 
                    Id = form.Id,
                    Nombre = form.Nombre,
                    Apellido = form.Apellido,
                    Telefono = form.Telefono,
                    Direccion = form.Direccion,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Cliente/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ClienteForm form)
        {
            try
            {
                Models.Sistema.ClienteModel model = new Models.Sistema.ClienteModel
                {
                    Id = form.Id,
                    Nombre = form.Nombre,
                    Apellido = form.Apellido,
                    Telefono = form.Telefono,
                    Direccion = form.Direccion,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Cliente/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.ELIMINAR, new Models.Sistema.ClienteModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario);
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
