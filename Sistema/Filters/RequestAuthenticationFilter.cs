using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sistema.Filters
{
    public class RequestAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var title = "No Autenticado";
            var code = 401;
            var error = "El tiempo de sesión se ha vencido.";
            var description = "Debe hacer login nuevamente si desea seguir navegando en el sitio.";
            bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            try
            {
                var session = context.HttpContext.Session.GetString("userToken");
                if (string.IsNullOrEmpty(session))
                {
                    if (!isAjax)
                    {
                        Console.WriteLine($"RequestAuthenticationFilter: No existe la sesión.");
                        context.Result = new RedirectToActionResult($"Exception", "Home", new { title = title, code = code, error = error, description = description });
                        base.OnActionExecuted(context);
                    }
                    else
                    {
                        Console.WriteLine($"RequestAuthenticationFilter: No existe la sesión.");
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult(new { Data = "401", title = title, code = code, error = error, description = description });
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RequestAuthenticationFilter: {ex.Message}.");
                context.Result = new RedirectToActionResult($"Exception", "Home", new { title = title, code = code, error = error, description = description });
                base.OnActionExecuted(context);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var title = "No Autenticado";
            var code = 401;
            var error = "El tiempo de sesión se ha vencido.";
            var description = "Debe hacer login nuevamente si desea seguir navegando en el sitio.";
            bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            try
            {
                var session = context.HttpContext.Session.GetString("userToken");
                if (string.IsNullOrEmpty(session))
                {
                    if (!isAjax)
                    {
                        Console.WriteLine($"RequestAuthenticationFilter: No existe la sesión.");
                        context.Result = new RedirectToActionResult($"Exception", "Home", new { title = title, code = code, error = error, description = description });
                        base.OnActionExecuting(context);
                    }
                    else
                    {
                        Console.WriteLine($"RequestAuthenticationFilter: No existe la sesión.");
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult(new { Data = "401", title = title, code = code, error = error, description = description });
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RequestAuthenticationFilter: {ex.Message}.");
                context.Result = new RedirectToActionResult($"Exception", "Home", new { title = title, code = code, error = error, description = description });
                base.OnActionExecuting(context);
            }
        }
    }
}
