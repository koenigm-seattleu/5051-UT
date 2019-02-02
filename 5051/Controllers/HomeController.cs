using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        /// <summary>
        /// The about page for AttendanceStar
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// The contact us page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// The Privacy Policy page
        /// </summary>
        /// <returns></returns>
        public ActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Help/Example page for Shop
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopExample()
        {
            return View();
        }

        /// <summary>
        /// Help/Example page for Student View
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentExample()
        {
            return View();
        }

        /// <summary>
        /// Help/Example page for Avatar House
        /// </summary>
        /// <returns></returns>
        public ActionResult HouseExample()
        {
            return View();
        }

        /// <summary>
        /// Help/Example page for Avatar Levels
        /// </summary>
        /// <returns></returns>
        public ActionResult AvatarExample()
        {
            return View();
        }

        /// <summary>
        /// Frequently Asked Questions
        /// </summary>
        /// <returns></returns>
        public ActionResult FAQ()
        {
            return View();
        }

        /// <summary>
        /// User Guide
        /// </summary>
        /// <returns></returns>
        public ActionResult Guide()
        {
            return View();
        }
    }
}