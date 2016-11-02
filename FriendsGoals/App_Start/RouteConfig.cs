using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FriendsGoals.Models;

namespace FriendsGoals
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Page",
				url: "{id}",
				defaults: new { controller = "Page", action = "Page", user = new ProfileModel() { Name = "Alexander", Surname = "Vashchilko", Sex = true } },
				constraints: new { id = @"\d+" }
			);

			routes.MapRoute(
				name: "Friends",
				url: "friends",
				defaults: new { controller = "Page", action = "Friends", user = new ProfileModel() { Name = "Alexander", Surname = "Vashchilko", Sex = true } }
			);

			routes.MapRoute(
				name: "Messages",
				url: "messages",
				defaults: new { controller = "Page", action = "Messages", user = new ProfileModel() { Name = "Alexander", Surname = "Vashchilko", Sex = true } }
			);

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
