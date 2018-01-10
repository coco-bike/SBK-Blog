using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace UI.Services
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string key ;
            if (filterContext.HttpContext.Request.Cookies["sessionId"] != null)
            {
                key = filterContext.HttpContext.Request.Cookies["sessionId"].Value;
                if (CacheHelper.Get(key) == null)
                {
                    filterContext.Result = new RedirectResult("../Web/WebLogin/index");
                }
                else
                {
                    filterContext.Result = new RedirectResult("../Web/Home/index");
                }
            }           
            base.OnActionExecuting(filterContext);
        }
    }
}