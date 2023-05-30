using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class InventarioController : NotificadorController
    {
        private readonly ILogger<InventarioController> _logger;
        protected const string _controller = "InventarioController";
        protected const string _dataTable = "DataTable/_InventarioTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private InventarioForm _form;

        public InventarioController(ILogger<InventarioController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new InventarioForm();
        }

        // GET: Inventario/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                (respuesta, mensaje, _form.productos) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Inventario/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(InventarioForm form)
        {
            try
            {
                Models.Sistema.InventarioModel model = new Models.Sistema.InventarioModel 
                { 
                    Id = form.Id,
                    ProductoId = form.ProductoId,
                    PrecioVenta = form.PrecioVenta,
                    Stock = form.Stock
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Inventario/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(InventarioForm form)
        {
            try
            {
                Models.Sistema.InventarioModel model = new Models.Sistema.InventarioModel
                {
                    Id = form.Id,
                    ProductoId = form.ProductoId,
                    PrecioVenta = form.PrecioVenta,
                    Stock = form.Stock
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.TODOS, new Models.Sistema.InventarioModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Inventario/Exists
        [HttpPost("Exists")]
        public IActionResult Exists(int ProductoId)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceInventario(Models.Sistema.OptionInventario.PRODUCTO_ID, new Models.Sistema.InventarioModel { ProductoId = ProductoId }, _usuario);

                return Json(data: _form.lista.Count == 0);
            }
            catch
            {
                return Json(data: false);
            }
        }
    }
}
