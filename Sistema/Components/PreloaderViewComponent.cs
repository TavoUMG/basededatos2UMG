using Sistema.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Components
{
    public class PreloaderViewComponent : ViewComponent
    {
        protected readonly ILogger<PreloaderViewComponent> _logger;

        public PreloaderViewComponent(ILogger<PreloaderViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                ModelPreoloaderView view = new ModelPreoloaderView();
                return View(model: view);
            }
            catch { return Content($"Error al cargar el componente Preloader!!!"); }
        }
    }
}
