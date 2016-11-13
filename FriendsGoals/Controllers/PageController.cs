using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;
using Microsoft.AspNet.Identity;


namespace FriendsGoals.Controllers
{
    public class PageController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public PageController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public PageController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult MyPage() => View();

		public ActionResult Page(ProfileModel user) => View(user);

		public ActionResult MyFriends() => View();

        public ActionResult AllUsers() => View(userManager.Users);

		public ActionResult Friends(ProfileModel user) => View(user);

		public ActionResult Messages() => View();
    }
}