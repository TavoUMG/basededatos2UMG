using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Models.Sistema;
using Sistema.Services;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class MarcketingController : NotificadorController
    {
        private readonly ILogger<MarcketingController> _logger;
        protected const string _controller = "MarcketingController";
        protected const string _dataTable = "DataTable/_MarcketingTable";
        private ServiceSQLServer _service;
        private MarcketingForm _form;

        public MarcketingController(ILogger<MarcketingController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _form = new MarcketingForm();
        }

        // GET: {factura}/Promociones/{productos}
        [HttpGet("{factura}/Promociones/{productos}")]
        public ActionResult Promociones(string factura, string productos)
        {
            try
            {
                Class.ClassSeguridad seguridad = new Class.ClassSeguridad();
                if (seguridad.DecryptData(factura).Split("-").Count() != 2) throw new Exception("Información inválida");

                string[] infoFactura = seguridad.DecryptData(factura).Split("-");
                List<FacturaModel> facturas = _service.ServiceFactura(OptionFactura.SELECCIONAR_ID, new FacturaModel { Id = Class.ClassUtilidad.parseMultiple(infoFactura[0], Class.ClassUtilidad.TipoDato.Integer).numero }).modelo;
                if (facturas.Count() == 0) throw new Exception("La factura no es válida");

                productos = seguridad.DecryptData(productos);

                _form.descuentos = _service.ServicePromocion(Models.Sistema.OptionPromocion.SELECCIONAR_ID, new Models.Sistema.PromocionModel { ProductosId = productos, TipoPromocionId = 1 }).modelo;
                _form.gratuitos = _service.ServicePromocion(Models.Sistema.OptionPromocion.SELECCIONAR_ID, new Models.Sistema.PromocionModel { ProductosId = productos, TipoPromocionId = 2 }).modelo;
                _form.mitad_precios = _service.ServicePromocion(Models.Sistema.OptionPromocion.SELECCIONAR_ID, new Models.Sistema.PromocionModel { ProductosId = productos, TipoPromocionId = 3 }).modelo;
                _form.compras_mayores = _service.ServicePromocion(Models.Sistema.OptionPromocion.SELECCIONAR_ID, new Models.Sistema.PromocionModel { ProductosId = productos, TipoPromocionId = 4 }).modelo;
                _form.puntos_productos = _service.ServicePromocion(Models.Sistema.OptionPromocion.SELECCIONAR_ID, new Models.Sistema.PromocionModel { ProductosId = productos, TipoPromocionId = 5 }).modelo;

                return View(viewName: "Promociones", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - {factura}/Promociones/{productos}");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Login", "Home");
            }
        }
    }
}
