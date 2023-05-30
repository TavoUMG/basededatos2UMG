using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class CajaController : NotificadorController
    {
        private readonly ILogger<CajaController> _logger;
        protected const string _controller = "CajaController";
        protected const string _dataTable = "DataTable/_CajaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private CajaForm _form;

        public CajaController(ILogger<CajaController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new CajaForm();
            _form.UsuarioId = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).ID;
        }

        // GET: Caja/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.TODOS, new Models.Sistema.CajaModel { }, _usuario);
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

        // POST: Caja/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(CajaForm form)
        {
            try
            {
                Models.Sistema.CajaModel model = new Models.Sistema.CajaModel 
                { 
                    Id = form.Id,
                    UsuarioId = form.UsuarioId,
                    efectivoApertura = form.efectivoApertura,
                    efectivoCierre = form.efectivoCierre,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.TODOS, new Models.Sistema.CajaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Caja/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(CajaForm form)
        {
            try
            {
                Models.Sistema.CajaModel model = new Models.Sistema.CajaModel
                {
                    Id = form.Id,
                    UsuarioId = form.UsuarioId,
                    efectivoApertura = form.efectivoApertura,
                    efectivoCierre = form.efectivoCierre,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.TODOS, new Models.Sistema.CajaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Caja/Cerrar
        [HttpPost("Cerrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar(CajaForm form)
        {
            try
            {
                Models.Sistema.CajaModel model = new Models.Sistema.CajaModel
                {
                    Id = form.Id,
                    UsuarioId = form.UsuarioId,
                    efectivoApertura = form.efectivoApertura,
                    efectivoCierre = form.efectivoCierre,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.CERRAR_CAJA, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.TODOS, new Models.Sistema.CajaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Cerrar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Caja/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.ELIMINAR, new Models.Sistema.CajaModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.TODOS, new Models.Sistema.CajaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Eliminar");
                return BadRequest(new { error = $"Ocurrio un error al eliminar: {ex.Message}" });
            }
        }

        // POST: Caja/Exists
        [HttpPost("Exists")]
        public IActionResult Exists()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { }, _usuario);

                return Json(data: _form.lista.Count == 0);
            }
            catch
            {
                return Json(data: false);
            }
        }
    }
}
