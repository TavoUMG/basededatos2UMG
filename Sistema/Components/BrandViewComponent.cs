using Sistema.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Components
{
    public class BrandViewComponent : ViewComponent
    {
        protected readonly ILogger<BrandViewComponent> _logger;

        public BrandViewComponent(ILogger<BrandViewComponent> logger)
        {
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                ModelBrandView view = new ModelBrandView();
                return View(model: view);
            }
            catch { return Content($"Error al cargar el componente Brand!!!"); }
        }
    }
}
