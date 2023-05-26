using Sistema.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Components
{
    public class UserPanelViewComponent : ViewComponent
    {
        protected readonly ILogger<UserPanelViewComponent> _logger;

        public UserPanelViewComponent(ILogger<UserPanelViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                ModelUserPanelView view = new ModelUserPanelView();
                view.user_name = HttpContext.Session.GetString("userName");
                return View(model: view);
            }
            catch { return Content($"Error al cargar el componente User Panel!!!"); }
        }
    }
}
