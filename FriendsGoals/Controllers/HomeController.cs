﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;

namespace FriendsGoals.Controllers
{
    public class HomeController : AppController
    {
        public ActionResult Index()
        {
            
            return View();
        }
        
    }
}