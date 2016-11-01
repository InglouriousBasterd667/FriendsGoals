using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;

namespace FriendsGoals.Controllers
{
    public class RegisterController : Controller
    {

        [HttpGet]
        public ViewResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ViewResult NewUser(Profile profile)
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