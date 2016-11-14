using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using FriendsGoals.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace FriendsGoals.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;


        public AuthController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public AuthController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }

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
        public async Task<ActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                await SignIn(user);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }
    

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        private async Task SignIn(AppUser user)
        {
            var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            //Here you can add any type of fields to current user.
            //Then go to appUserPrincipal and add new field
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.UserSurname));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
			identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.Phone));
			identity.AddClaim(new Claim(ClaimTypes.Gender, user.Sex ? "Male" : "Female"));
            GetAuthenticationManager().SignIn(identity);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }





        [HttpGet]
        public ViewResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(ProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new AppUser
            {
                UserName = model.Email,
                UserSurname = model.Surname,
                Name = model.Name,
                Phone = model.Phone,
                Sex = (bool)model.Sex,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }
    }
    
}