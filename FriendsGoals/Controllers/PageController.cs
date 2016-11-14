using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;

namespace FriendsGoals.Controllers
{
    public class PageController : Controller
    {
		public ActionResult MyPage() => View();

		public ActionResult Page(ProfileModel user) => View(user);

		public ActionResult MyFriends() => View();

		public ActionResult Friends(ProfileModel user) => View(user);

		public ActionResult Messages() => View();
    }
}