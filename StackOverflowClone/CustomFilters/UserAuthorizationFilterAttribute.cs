using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowClone.CustomFilters
{
    public class UserAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization (AuthorizationContext context)
        {
            //Check if user don't login/sign up, redirect to Home
            if (context.RequestContext.HttpContext.Session["CurrentUserName"] == null)
            {
                context.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }
}