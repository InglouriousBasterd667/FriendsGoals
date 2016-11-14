using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FriendsGoals.Controllers
{
    public class FileController : AppController
    {
        private readonly UserManager<AppUser> userManager;
        public FileController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public FileController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        private AppDbContext db = new AppDbContext();
        // GET: File
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}