using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _5051.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("timezoneoffset"))
            {
                if (HttpContext != null &&
                    HttpContext.Request != null &&
                   HttpContext.Request.Cookies["timezoneoffset"] != null &&
                    !string.IsNullOrEmpty(HttpContext.Request.Cookies["timezoneoffset"].Value))
                {
                    Session["timezoneoffset"] =
                        HttpContext.Request.Cookies["timezoneoffset"].Value;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}