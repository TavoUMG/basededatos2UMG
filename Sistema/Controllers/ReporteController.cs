using Microsoft.AspNetCore.Mvc;
using Sistema.Filters;
using Sistema.Models.Formulario;
using Sistema.Services;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    [RequestAuthenticationFilter]
    public class ReporteController : NotificadorController
    {
        private readonly ILogger<ReporteController> _logger;
        protected const string _controller = "ReporteController";
        private ServiceSQLServer _service;
        private String _usuario;
        private ReporteForm _form;

        public ReporteController(ILogger<ReporteController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _form = new ReporteForm();
        }

        // GET: Reporte/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Reporte/Index
        [HttpPost("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ReporteForm form)
        {
            try
            {
                (bool respuesta, string mensaje, _form.Ventas) = _service.ServiceReporteVenta(form);
                if (!respuesta) throw new Exception(mensaje);

                (respuesta, mensaje, _form.Cajas) = _service.ServiceReporteCaja(form);
                if (!respuesta) throw new Exception(mensaje);

                (respuesta, mensaje, _form.Compras) = _service.ServiceReporteCompra(form);
                if (!respuesta) throw new Exception(mensaje);

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Reporte");
            }
        }
    }
}
