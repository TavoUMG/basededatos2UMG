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
    public class CategoriaController : NotificadorController
    {
        private readonly ILogger<CategoriaController> _logger;
        protected const string _controller = "CategoriaController";
        protected const string _dataTable = "DataTable/_CategoriaTable";
        private ServiceSQLServer _service;
        private String _usuario;
        private CategoriaForm _form;

        public CategoriaController(ILogger<CategoriaController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _service = new ServiceSQLServer();
            _usuario = WebUtil.GetServiceValues(httpContextAccessor.HttpContext.Session).NombreCompleto;
            _form = new CategoriaForm();
        }

        // GET: Categoria/Index
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.TODOS, new Models.Sistema.CategoriaModel { }, _usuario);
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

        // POST: Categoria/Insertar
        [HttpPost("Insertar")]
        [ValidateAntiForgeryToken]
        public ActionResult Insertar(CategoriaForm form)
        {
            try
            {
                Models.Sistema.CategoriaModel model = new Models.Sistema.CategoriaModel 
                { 
                    Id = form.Id,
                    Nombre = form.Nombre
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.CREAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.TODOS, new Models.Sistema.CategoriaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Insertar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Categoria/Actualizar
        [HttpPost("Actualizar")]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(CategoriaForm form)
        {
            try
            {
                Models.Sistema.CategoriaModel model = new Models.Sistema.CategoriaModel
                {
                    Id = form.Id,
                    Nombre = form.Nombre
                };

                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.EDITAR, model, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.TODOS, new Models.Sistema.CategoriaModel { }, _usuario);
                if (!respuesta) throw new Exception(mensaje);

                return PartialView(viewName: _dataTable, model: _form);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(exception: ex, message: $"{_controller}  - Actualizar");
                return BadRequest(new { error = $"Ocurrio un error al guardar: {ex.Message}" });
            }
        }

        // POST: Categoria/Eliminar
        [HttpPost("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int Id)
        {
            try
            {
                (bool respuesta, string mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.ELIMINAR, new Models.Sistema.CategoriaModel { Id  = Id }, _usuario);
                if (!respuesta) throw new Exception(mensaje);
                (respuesta, mensaje, _form.lista) = _service.ServiceCategoria(Models.Sistema.OptionCategoria.TODOS, new Models.Sistema.CategoriaModel { }, _usuario);
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
