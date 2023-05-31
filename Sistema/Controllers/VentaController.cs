using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class VentaController : NotificadorController
    {
        private readonly ILogger<VentaController> _logger;
        protected const string _controller = "VentaController";
        protected const string _dataTable = "DataTable/_VentaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private FacturaForm _form;

        public VentaController(ILogger<VentaController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new FacturaForm();
        }

        // GET: Venta/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceFactura(Models.Sistema.OptionFactura.TODOS, new Models.Sistema.FacturaModel { }, _usuario);
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
    }
}
