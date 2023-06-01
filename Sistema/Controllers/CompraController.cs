using Microsoft.AspNetCore.Mvc;
using Sistema.Models.Formulario;
using Sistema.Services;
using Sistema.Util;
using static Sistema.Models.View.ModelSweetAlert;

namespace Sistema.Controllers
{
    [Route("[controller]")]
    public class CompraController : NotificadorController
    {
        private readonly ILogger<CompraController> _logger;
        protected const string _controller = "CompraController";
        protected const string _dataTable = "DataTable/_CompraTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private CompraForm _form;

        public CompraController(ILogger<CompraController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new CompraForm();
        }

        // GET: Compra/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.TODOS, new Models.Sistema.CompraModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                (respuesta, mensaje, _form.productos) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);
                (respuesta, mensaje, _form.proveedores) = _service.ServiceProveedor(Models.Sistema.OptionProveedor.TODOS, new Models.Sistema.ProveedorModel { }, _usuario);

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Compra/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(CompraForm form)
        {
            try
            {
                Models.Sistema.CompraModel model = new Models.Sistema.CompraModel
                {
                    Id = form.Id,
                    Cantidad = form.Cantidad,
                    ProductoId = form.ProductoId,
                    ProveedorId = form.ProveedorId,
                    PrecioCosto = form.PrecioCosto,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.TODOS, new Models.Sistema.CompraModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Producto/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(CompraForm form)
        {
            try
            {
                Models.Sistema.CompraModel model = new Models.Sistema.CompraModel
                {
                    Id = form.Id,
                    Cantidad = form.Cantidad,
                    ProductoId = form.ProductoId,
                    ProveedorId = form.ProveedorId,
                    PrecioCosto = form.PrecioCosto,
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.TODOS, new Models.Sistema.CompraModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Producto/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.ELIMINAR, new Models.Sistema.CompraModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCompra(Models.Sistema.OptionCompra.TODOS, new Models.Sistema.CompraModel { }, _usuario);
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
