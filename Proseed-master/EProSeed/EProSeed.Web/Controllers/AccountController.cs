using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using EProSeed.Models;
using EProSeed.Lib.BLL;

namespace EProSeed.Web.Controllers
{
    public class AccountController : Controller
    {
        public void SignIn()
        {
            // Send an OpenID Connect sign-in request.
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                 RedirectToAction("Index", "Home");
            }
        }

        public void SignOut()
        {
            string callbackUrl = Url.Action("SignOutCallback", "Account", routeValues: null, protocol: Request.Url.Scheme);

            HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        public ActionResult SignOutCallback()
        {
            if (Request.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private void CreateSession(UserType userType, EProSeed.Models.TrainerModel user)
        {
            Session["Name"] = user.Name;
            Session["UserType"] = userType.ToString();
            Session["UserId"] = user.Id.ToString();
            Session["UserEmailId"] = user.Email.ToString();
        }

       

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult Login(vmLogin model, string returnUrl)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            UserType userType;
        //            var User = _Trainer.Login(model.Email, model.Password, out userType);
        //            if (User != null)
        //            {
        //                CreateSession(userType, User);

        //                FormsAuthentication.SetAuthCookie(
        //                        User.Email, false);

        //                FormsAuthenticationTicket ticket1 =
        //                   new FormsAuthenticationTicket(
        //                        1,
        //                        User.Email,
        //                        DateTime.Now,
        //                        DateTime.Now.AddMinutes(20),
        //                        false, User.Email);
        //                HttpCookie cookie1 = new HttpCookie(
        //                  FormsAuthentication.FormsCookieName,
        //                  FormsAuthentication.Encrypt(ticket1));
        //                Response.Cookies.Add(cookie1);
        //                FormsAuthentication.RedirectFromLoginPage(User.Email, true);
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        ViewData["Error"] = "Invalid Email or Password.";
        //    }

        //    catch (Exception ex)
        //    {
        //        ViewData["Error"] = ex.Message;
        //    }
        //    return View(model);
        //}

    }
}