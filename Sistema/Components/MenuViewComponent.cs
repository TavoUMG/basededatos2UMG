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

                MENUITEM item = new MENUITEM();

                //Usuario
                item = new MENUITEM() { controller = "Usuario", action = "Index", name = "Usuarios", icon = "fa-solid fa-circle" };
                menu.items.Add(item);
                item = new MENUITEM() { controller = "Usuario", action = "Insertar", name = "Usuario Insertar", icon = "fa-solid fa-circle" };
                menu.items.Add(item);
                item = new MENUITEM() { controller = "Usuario", action = "Actualizar", name = "Usuario Actualizar", icon = "fa-solid fa-circle" };
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
