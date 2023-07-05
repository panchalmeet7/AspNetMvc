using AspNetMvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc.Controllers
{
    [ExceptionFilter] //Controller level exception filter
    //[UserAuthenticationFilter]
    public class HomeController : Controller
    {
        //[ExceptionFilter] Action method exception filter
        public ActionResult Index()
        {
            return View();
            //throw new ArgumentOutOfRangeException();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult TestRange()
        {
            return View();
        }
    }
}