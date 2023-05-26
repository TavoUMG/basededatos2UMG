using Sistema.Models.View;
using Sistema.Util;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Components
{
    public class MenuViewComponent : ViewComponent
    {
        protected readonly ILogger<MenuViewComponent> _logger;

        public MenuViewComponent(ILogger<MenuViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                ModelMenuView menu = new ModelMenuView();

                var item = new MENUITEM() { controller = "Recetas", action = "ListaRecetas", name = "Listado de Recetas", icon = "fa-solid fa-file-prescription" };
                menu.items.Add(item);

                item = new MENUITEM() { controller = "Home", action = "Logout", name = "Salir", icon = "fa-solid fa-door-open"};
                menu.items.Add(item);

                try
                {
                    var idUsuario = int.Parse(HttpContext.Session.GetString("userId"));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(exception: ex, message: $"MenuViewComponent");
                }
                return View(model: menu);
            }
            catch { return Content($"Error al cargar el componente Menu!!!"); }
        }
    }
}
