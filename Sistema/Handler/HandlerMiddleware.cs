using Sistema.Class;
using Sistema.Models;
using System.Web;

namespace Sistema.Handler
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public HandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            ErrorPersonalizedViewModel model;
            (string title, string error, int code, string description) = ("", "", 0, "");

            try
            {
                await _next(httpContext);
            }
            catch (BadHttpRequestException ex)
            {
                model = new ErrorPersonalizedViewModel(TypeError.ERROR400);
                title = HttpUtility.UrlEncode(model.title);
                error = HttpUtility.UrlEncode(model.error);
                code = ClassUtilidad.parseMultiple(HttpUtility.UrlEncode(model.code.ToString()), ClassUtilidad.TipoDato.Integer).numero;
                description = HttpUtility.UrlEncode(ex.Message);
                httpContext.Response.Redirect($"/Exception/Screen?title={title}&error={error}&code={code}&description={description}");
            }
            catch (UnauthorizedAccessException ex)
            {
                model = new ErrorPersonalizedViewModel(TypeError.ERROR401);
                title = HttpUtility.UrlEncode(model.title);
                error = HttpUtility.UrlEncode(model.error);
                code = ClassUtilidad.parseMultiple(HttpUtility.UrlEncode(model.code.ToString()), ClassUtilidad.TipoDato.Integer).numero;
                description = HttpUtility.UrlEncode(ex.Message);
                httpContext.Response.Redirect($"/Exception/Screen?title={title}&error={error}&code={code}&description={description}");
            }
            catch (TimeoutException ex)
            {
                model = new ErrorPersonalizedViewModel(TypeError.ERROR408);
                title = HttpUtility.UrlEncode(model.title);
                error = HttpUtility.UrlEncode(model.error);
                code = ClassUtilidad.parseMultiple(HttpUtility.UrlEncode(model.code.ToString()), ClassUtilidad.TipoDato.Integer).numero;
                description = HttpUtility.UrlEncode(ex.Message);
                httpContext.Response.Redirect($"/Exception/Screen?title={title}&error={error}&code={code}&description={description}");
            }
            catch (DirectoryNotFoundException ex)
            {
                model = new ErrorPersonalizedViewModel(TypeError.ERROR404);
                title = HttpUtility.UrlEncode(model.title);
                error = HttpUtility.UrlEncode(model.error);
                code = ClassUtilidad.parseMultiple(HttpUtility.UrlEncode(model.code.ToString()), ClassUtilidad.TipoDato.Integer).numero;
                description = HttpUtility.UrlEncode(ex.Message);
                httpContext.Response.Redirect($"/Exception/Screen?title={title}&error={error}&code={code}&description={description}");
            }
            catch (Exception ex)
            {
                model = new ErrorPersonalizedViewModel(TypeError.ERROR500);
                title = HttpUtility.UrlEncode(model.title);
                error = HttpUtility.UrlEncode(model.error);
                code = ClassUtilidad.parseMultiple(HttpUtility.UrlEncode(model.code.ToString()), ClassUtilidad.TipoDato.Integer).numero;
                description = HttpUtility.UrlEncode(ex.Message);
                httpContext.Response.Redirect($"/Exception/Screen?title={title}&error={error}&code={code}&description={description}");
            }
        }
    }
}
