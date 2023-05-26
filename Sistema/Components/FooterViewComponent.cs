using Sistema.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Components
{
    public class FooterViewComponent : ViewComponent
    {
        protected readonly ILogger<FooterViewComponent> _logger;

        public FooterViewComponent(ILogger<FooterViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                ModelApplicationView view = new ModelApplicationView();
                view.actionController = HttpContext.Session.GetString("action") != null ? HttpContext.Session.GetString("action") : "Index";
                return View(model: view);
            }
            catch { return Content($"Error al cargar el componente Foooter!!!"); }
        }
    }
}
