using Microsoft.AspNet.Identity.EntityFramework;

namespace FriendsGoals
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string UserSurname { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }


    }
}
