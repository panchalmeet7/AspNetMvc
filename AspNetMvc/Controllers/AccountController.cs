using Entities.ViewModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegisterUser _registerUser;

        private string connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ToString();
        }
        public AccountController(IRegisterUser registerUser)
        {
            _registerUser = registerUser;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var constr = connection();
                _registerUser.Register(model, constr);
            }

            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}