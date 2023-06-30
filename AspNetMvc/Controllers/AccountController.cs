using AspNetMvc.Filters;
using Entities.Models;
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
            //"Data Source= PCT38\\SQL2019; Integrated Security=true;Initial Catalog= DemoProjectDB; User Id =sa; Password=Tatva@123; "
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
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Test", "Account");
            }
            return View();
        }

        [HttpGet]
        [UserAuthenticationFilter]
        public ActionResult Test(LoginViewModel model)
        {
            if (Session["UserID"] == null)
            {
                //return View("Error", "_Layout");
                return View("~/Views/Shared/Error.cshtml");
            }
            return View();
        }

        [HttpPost]
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
                User Role = await _loginuser.UserLogin(model, constr);
                if (Role == null)
                {
                    TempData["Error"] = "Invalid Credentials!";
                    return View();
                }
                else if (Role.Role == "USER")
                {
                    Session["UserID"] = Guid.NewGuid();
                    return RedirectToAction("Test", "Account");
                }
                else if (Role.Role == "ADMIN")
                {
                    return RedirectToAction("Test", "Admin");
                }
            }

            ModelState.Clear();
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}