using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EProSeed.Web
{
    using DAL;
    using EProSeed.Models;
    using System;
    using System.Web;
    using System.Web.Security;


    internal static class SingleSessionPreparation
    {
        internal static void CreateAndStoreSessionToken(string mail)
        {
            HttpResponse pageResponse = HttpContext.Current.Response;

            Guid sessionToken = System.Guid.NewGuid();

            HttpCookie authenticationCookie =
                pageResponse.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket authenticationTicket =
                FormsAuthentication.Decrypt(authenticationCookie.Value);

            FormsAuthenticationTicket newAuthenticationTicket =
                new FormsAuthenticationTicket(
                authenticationTicket.Version,
                authenticationTicket.Name,
                authenticationTicket.IssueDate,
                authenticationTicket.Expiration,
                authenticationTicket.IsPersistent,
                sessionToken.ToString(),
                authenticationTicket.CookiePath);

            // Replace the authentication cookie
            pageResponse.Cookies.Remove(FormsAuthentication.FormsCookieName);

            HttpCookie newAuthenticationCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(newAuthenticationTicket));

            newAuthenticationCookie.HttpOnly = authenticationCookie.HttpOnly;
            newAuthenticationCookie.Path = authenticationCookie.Path;
            newAuthenticationCookie.Secure = authenticationCookie.Secure;
            newAuthenticationCookie.Domain = authenticationCookie.Domain;
            newAuthenticationCookie.Expires =DateTime.Now.AddMinutes(20);

            pageResponse.Cookies.Add(newAuthenticationCookie);
        }
    }
}