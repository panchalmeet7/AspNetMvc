using Entities.ViewModels;
using Microsoft.Owin.Security.Cookies;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        public static string connection()
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

        // Registration 
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var constr = connection();
                int status = await _registerUser.EmailCheck(model, constr);
                if (status == 1)
                {
                    ViewBag.Message = "Email already exists, Please try with another Email!";
                }
                else
                {
                   await _registerUser.Register(model, constr);
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        /// <param name="model"></param>
        /// <returns> error if email pass does not match </returns>
        // Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var constr = connection();
                int status = await _loginuser.UserLogin(model, constr);
                if (status == 1)
                {
                    Session["UserID"] = Guid.NewGuid();
                    return RedirectToAction("Test", "Account");
                }
                else
                {
                    TempData["Error"] = "Invalid Credentials!";
                    return View();
                }
            }

            ModelState.Clear();
            return View();
        }
    }
}