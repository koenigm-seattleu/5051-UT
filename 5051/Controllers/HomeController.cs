using System.Web.Mvc;

namespace _5051.Controllers
{
    /// <summary>
    /// Home Controller is the default controller
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// The Home page for the site
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Error page for the site
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }
    }
}