using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Sistema
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("tag-name")]
    public static class MenuStateHelper 
    {
        public static string? MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
        {
            try
            {
                string result = "active";

                if (!String.IsNullOrEmpty(Convert.ToString(urlHelper.ActionContext.RouteData.DataTokens["area"])))
                {
                    string areaName = urlHelper.ActionContext.RouteData.DataTokens["area"].ToString();
                }

                if (urlHelper.ActionContext.RouteData.Values != null)
                {
                    string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
                    string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
                    if (string.IsNullOrEmpty(controllerName)) return null;
                    if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
                    {
                        if (controllerName.Equals("Home"))
                        {
                            if (methodName != null && methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
                            {
                                return result;
                            }
                        }
                        else
                            return result;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
