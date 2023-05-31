using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class FacturaController : NotificadorController
    {
        private readonly ILogger<FacturaController> _logger;
        protected const string _controller = "FacturaController";
        protected const string _dataTable = "DataTable/_FacturaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private FacturaForm _form;

        public FacturaController(ILogger<FacturaController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new FacturaForm();
        }

        // GET: Factura/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            { 
                _form.Detalle = new List<DatelleForm>();
                _form.clientes = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario).modelo;
                _form.productos = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario, true).modelo;

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Factura/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(FacturaForm form)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                Alert("¡Error!", ex.Message, NotificationType.error);
            }

            return RedirectToAction(actionName: "Index", controllerName: "Factura");
        }
    }
}
