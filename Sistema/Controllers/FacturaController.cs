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
    public class FacturaController : NotificadorController
    {
        private readonly ILogger<FacturaController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected const string _controller = "FacturaController";
        protected const string _dataTable = "DataTable/_FacturaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private FacturaForm _form;
        private int _UsuarioId;

        public FacturaController(ILogger<FacturaController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new FacturaForm();
            _hostEnvironment = hostEnvironment;
            _UsuarioId = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).ID;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Factura/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                List<Models.Sistema.CajaModel> cajas = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { UsuarioId = _UsuarioId }, _usuario).modelo;
                if (cajas.Count == 0)
                {
                    AlertSuperior("Este usuario no tiene caja abierta, cree una caja por favor.", NotificationType.info);
                    return RedirectToAction($"Index", "Caja");
                }

                CargarCatalogos();

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
                List<Models.Sistema.CajaModel> cajas = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { UsuarioId = _UsuarioId }, _usuario).modelo;
                if (cajas.Count == 0) throw new Exception("Este usuario aún no a abierto una caja, vaya a la caja a crear una por favor.");

                form.CajaId = cajas[0].Id;
                form.Total = 0;
                for (int i = 0; i < form.Detalle.Count; i++)
                {
                    form.Total += form.Detalle[i].SubTotal;
                }

                (bool respuesta, string mensaje, List<Models.Sistema.FacturaModel> factura, List<Models.Sistema.DetalleModel> detalle) = _service.ServiceTransactionFactura(form, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                form.Numero = factura[0].Numero;
                form.Archivo = factura[0].Archivo;

                string host = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";
                //Generamos
                if (WebUtil.generarPDF(_hostEnvironment.WebRootPath, factura[0], detalle, host))
                {
                    _form = form;
                    CargarCatalogos(imprimir: true);
                    return View(viewName: "Index", model: _form);
                }
                else
                {
                    throw new Exception("El comprobante no pudo crearse.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                Alert("¡Error!", ex.Message, NotificationType.error);

                return RedirectToAction(actionName: "Index", controllerName: "Factura");
            }
        }

        private void CargarCatalogos(bool imprimir = false)
        { 
            if(!imprimir)
            {
                _form.Detalle = new List<DatelleForm>();
            }

            _form.pagos.Add(new Select { value = "CONTADO" });
            _form.pagos.Add(new Select { value = "TARJETA" });
            _form.pagos.Add(new Select { value = "CREDITO" });
            _form.pagos.Add(new Select { value = "PUNTOS" });
            
            _form.clientes = _service.ServiceCliente(Models.Sistema.OptionCliente.TODOS, new Models.Sistema.ClienteModel { }, _usuario).modelo;
            _form.productos = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario, true).modelo;
            _form.Imprimir = imprimir;
        }
    }
}
