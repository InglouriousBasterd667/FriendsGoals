using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FriendsGoals.Models;



namespace FriendsGoals.Controllers
{
    public class HomeController : AppController
    {
        public AppDbContext adc;


        public ActionResult Index()
        {
            //AppUser user1 = adc.Users.FirstOrDefault(x => x.UserName == "pedik@gmail.com");
            //AppUser user2 = adc.Users.FirstOrDefault(x => x.UserName == "pedik@gmail.com");
            //user1.Friends.Add(user2);
            //adc.SaveChanges();
           // userManager.Users.FirstOrDefault(x => x.UserName == "pedik@gmail.com");

            return View();
        }

        
    }
}