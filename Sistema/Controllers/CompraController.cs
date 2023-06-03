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
    public class CompraController : NotificadorController
    {
        private readonly ILogger<CompraController> _logger;
        protected const string _controller = "CompraController";
        protected const string _dataTable = "DataTable/_CompraTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private CompraForm _form;
        private int _UsuarioId;

        public CompraController(ILogger<CompraController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new CompraForm();
            _UsuarioId = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).ID;
        }

        // GET: Compra/Index
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
                List<Models.Sistema.CajaModel> cajas = _service.ServiceCaja(Models.Sistema.OptionCaja.CAJA_ABIERTA, new Models.Sistema.CajaModel { UsuarioId = _UsuarioId }, _usuario).modelo;
                if (cajas.Count == 0) throw new Exception("Este usuario aún no a abierto una caja, vaya a la caja a crear una por favor.");

                Models.Sistema.CompraModel model = new Models.Sistema.CompraModel
                {
                    Id = form.Id,
                    Cantidad = form.Cantidad,
                    ProductoId = form.ProductoId,
                    ProveedorId = form.ProveedorId,
                    PrecioCosto = form.PrecioCosto,
                    CajaId = cajas[0].Id
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
