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
    public class ProductoController : NotificadorController
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        protected const string _controller = "ProductoController";
        protected const string _dataTable = "DataTable/_ProductoTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private ProductoForm _form;

        public ProductoController(ILogger<ProductoController> logger, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new ProductoForm();
        }

        // GET: Producto/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                (respuesta, mensaje, _form.categorias) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.TODOS, new Models.Sistema.CategoriaModel { }, _usuario);

                return View(viewName: "Index", model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Index");
                AlertSuperior("Ha ocurrido un error al cargar la pantalla", NotificationType.error);
                return RedirectToAction($"Index", "Home");
            }
        }

        // POST: Producto/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(ProductoForm form)
        {
            try
            {
                (bool respuesta, string mensaje) = WebUtil.guardarImagen(Path.Combine(_hostEnvironment.WebRootPath, "Productos"), form.Imagen);
                if (!respuesta) throw new Exception(mensaje);

                Models.Sistema.ProductoModel model = new Models.Sistema.ProductoModel 
                { 
                    Id = form.Id,
                    CategoriaId = form.CategoriaId,
                    Nombre = form.Nombre,
                    Vencimiento = form.Vencimiento,
                    Imagen = mensaje
                };

                (respuesta, mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);
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
        public ActionResult Actualizar(ProductoForm form)
        {
            try
            {
                (bool respuesta, string mensaje) = WebUtil.guardarImagen(Path.Combine(_hostEnvironment.WebRootPath, "Productos"), form.Imagen, form.Imagen != null);
                if(!respuesta) throw new Exception(mensaje);

                Models.Sistema.ProductoModel model = new Models.Sistema.ProductoModel
                {
                    Id = form.Id,
                    CategoriaId = form.CategoriaId,
                    Nombre = form.Nombre,
                    Vencimiento = form.Vencimiento,
                    Imagen = mensaje
                };

                (respuesta, mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);
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
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.ELIMINAR, new Models.Sistema.ProductoModel { Id = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceProducto(Models.Sistema.OptionProducto.TODOS, new Models.Sistema.ProductoModel { }, _usuario);
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
