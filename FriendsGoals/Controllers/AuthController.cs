using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FriendsGoals.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // this really bad
            if (model.Email == "admin@admin.com" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Ben"),
                new Claim(ClaimTypes.Email, "a@b.com"),
                new Claim(ClaimTypes.Country, "England")
            },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        [HttpGet]
        public ViewResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ViewResult NewUser(ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                // TODO: Email response to the party organizer
                return View("Thanks", profile);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }
    }
}