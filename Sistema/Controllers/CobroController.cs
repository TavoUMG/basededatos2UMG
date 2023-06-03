using Microsoft.AspNetCore.Mvc;
using Sistema.Filters;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    [RequestAuthenticationFilter]
    public class CobroController : NotificadorController
    {
        private readonly ILogger<CobroController> _logger;
        protected const string _controller = "CobroController";
        protected const string _dataTable = "DataTable/_CobroTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private CobroForm _form;

        public CobroController(ILogger<CobroController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new CobroForm();
        }

        // GET: Cobro/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.TODOS, new Models.Sistema.CobroModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                _form.facturas = _service.ServiceCobro(Models.Sistema.OptionCobro.FACTURA_PENDIENTE_PAGO, new Models.Sistema.CobroModel { }, _usuario).factura;

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Cobro/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(CobroForm form)
        {
            try
            {
                Models.Sistema.CobroModel model = new Models.Sistema.CobroModel
                {
                    Id = form.Id,
                    Monto = form.Monto,
                    FacturaId = form.FacturaId
                };

                (bool respuesta, string mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.MONTO_RESTANTE, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                decimal MontoRestante = _form.lista[0].MontoRestante;

                if (model.Monto > (MontoRestante + 1)) throw new Exception($"El monto total a pagar es Q ${model.Monto} y no puede dar más que el monto restante Q {_form.lista[0].MontoRestante}");

                (respuesta, mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                if((model.Monto - MontoRestante) == 0) return RedirectToAction($"Index", "Cobro");

                (respuesta, mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.TODOS, new Models.Sistema.CobroModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Cobro/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.ELIMINAR, new Models.Sistema.CobroModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista, _form.facturas) = _service.ServiceCobro(Models.Sistema.OptionCobro.TODOS, new Models.Sistema.CobroModel { }, _usuario);
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
