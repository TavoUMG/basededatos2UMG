﻿using Sistema.Models.View;
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
                item = new MENUITEM() { controller = "Usuario", action = "Index", name = "Usuarios", icon = "fa-solid fa-users" };
                menu.items.Add(item);

                //Cliente
                item = new MENUITEM() { controller = "Cliente", action = "Index", name = "Clientes", icon = "fa-solid fa-user-tie" };
                menu.items.Add(item);

                //Proveedor
                item = new MENUITEM() { controller = "Proveedor", action = "Index", name = "Proveedores", icon = "fa-solid fa-user-tag" };
                menu.items.Add(item);

                //Categoría
                item = new MENUITEM() { controller = "Categoria", action = "Index", name = "Categorías", icon = "fa-solid fa-bookmark" };
                menu.items.Add(item);

                //Producto
                item = new MENUITEM() { controller = "Producto", action = "Index", name = "Productos", icon = "fa-solid fa-truck-front" };
                menu.items.Add(item);

                //Caja
                item = new MENUITEM() { controller = "Caja", action = "Index", name = "Cajas", icon = "fa-solid fa-circle" };
                menu.items.Add(item);

                //Inventario
                item = new MENUITEM() { controller = "Inventario", action = "Index", name = "Inventarios", icon = "fa-solid fa-shop" };
                menu.items.Add(item);

                //Compra
                item = new MENUITEM() { controller = "Compra", action = "Index", name = "Compras", icon = "fa-solid fa-circle" };
                menu.items.Add(item);

                //Venta
                item = new MENUITEM() { controller = "Venta", action = "Index", name = "Ventas", icon = "fa-solid fa-circle" };
                menu.items.Add(item);

                //Devolución
                item = new MENUITEM() { controller = "Devolucion", action = "Index", name = "Devoluciones", icon = "fa-solid fa-circle" };
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
