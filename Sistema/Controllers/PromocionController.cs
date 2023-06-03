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
    public class PromocionController : NotificadorController
    {
        private readonly ILogger<PromocionController> _logger;
        protected const string _controller = "PromocionController";
        protected const string _dataTable = "DataTable/_PromocionTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private PromocionForm _form;

        public PromocionController(ILogger<PromocionController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new PromocionForm();
        }

        // GET: Promocion/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.TODOS, new Models.Sistema.PromocionModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                _form.promociones = _service.ServiceTipoPromocion(Models.Sistema.OptionTipoPromocion.TODOS, new Models.Sistema.TipoPromocionModel { }).modelo;
                _form.productos = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario).modelo;

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Promocion/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(PromocionForm form)
        {
            try
            {
                Models.Sistema.PromocionModel model = new Models.Sistema.PromocionModel
                {
                    Id = form.Id,
                    ProductoId = form.ProductoId,
                    TipoPromocionId = form.TipoPromocionId,
                    Descripcion = form.Descripcion
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.TODOS, new Models.Sistema.PromocionModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Promocion/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(PromocionForm form)
        {
            try
            {
                Models.Sistema.PromocionModel model = new Models.Sistema.PromocionModel
                {
                    Id = form.Id,
                    ProductoId = form.ProductoId,
                    TipoPromocionId = form.TipoPromocionId,
                    Descripcion = form.Descripcion
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.TODOS, new Models.Sistema.PromocionModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Promocion/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.ELIMINAR, new Models.Sistema.PromocionModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServicePromocion(Models.Sistema.OptionPromocion.TODOS, new Models.Sistema.PromocionModel { }, _usuario);
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
