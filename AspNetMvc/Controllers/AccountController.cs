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
        private readonly ILoginUser _loginuser;

        private string connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ToString();
        }
        public AccountController(IRegisterUser registerUser, ILoginUser loginuser)
        {
            _registerUser = registerUser;
            _loginuser = loginuser;
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Test()
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

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var constr = connection();
                int status = _loginuser.UserLogin(model, constr);
                if (status == 1)
                {
                    return RedirectToAction("Test", "Account");
                }
                else
                {
                    TempData["Error"] = "Invalid Credentials!!";
                    return View();
                }
            }

            ModelState.Clear();
            return View();
        }
    }
}