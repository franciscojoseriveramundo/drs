using DRS.PostgreSQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRS.UI.Helpers
{
    public class Authenticate: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                base.OnActionExecuting(filterContext);

                RedirectToLogin routeValues = new RedirectToLogin()
                {
                    alert = "alert alert-warning",
                    message = "No ha iniciado sesión, ingrese sus credenciales de acceso."
                };

                RouteValueDictionary result =  new RouteValueDictionary()
                {
                    { "controller", "Home" },
                    { "action", "Index" },
                    { "mensaje", routeValues.message },
                    { "alert", routeValues.alert }
                };

                filterContext.Result = new RedirectToRouteResult(result);

            }
            else
            {
                //string username = filterContext.HttpContext.Session.GetString("Username");

                //if (string.IsNullOrEmpty(username))
                //{
                //    filterContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //}

                var action = filterContext.ActionDescriptor.RouteValues["action"].Replace("Ini", "");
                string controller = filterContext.Controller.GetType().Name.Replace("Controller", "");
                

            }
            
        }
    }

    public class NoAuthenticate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
    
            var user = filterContext.HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                base.OnActionExecuting(filterContext);

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Devoluciones",
                    action = "Index"
                }));
            }
            
        }
    }

    
}
