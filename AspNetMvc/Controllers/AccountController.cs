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
        #region Properties
        private readonly IRegisterUser _registerUser;
        private readonly ILoginUser _loginuser;
        #endregion

        #region Getting Connection String
        private string connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ToString();
        }
        #endregion

        #region Constructor
        public AccountController(IRegisterUser registerUser, ILoginUser loginuser)
        {
            _registerUser = registerUser;
            _loginuser = loginuser;
        }
        #endregion

        #region Get Methods
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
        #endregion

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var constr = connection();
                int status = _registerUser.EmailCheck(model, constr);
                if (status == 1)
                {
                    TempData["Error"] = "Email already exists, Please try with another Email!!";
                }
                else
                {
                    _registerUser.Register(model, constr);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        /// <param name="model"></param>
        /// <returns> error if email pass does not match </returns>
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