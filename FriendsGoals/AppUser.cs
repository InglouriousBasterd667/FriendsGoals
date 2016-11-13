using Microsoft.AspNet.Identity.EntityFramework;
using FriendsGoals.Models;
using System.Collections.Generic;

namespace FriendsGoals
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string UserSurname { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }

        public ICollection<AppUser> Friends { get; set; }
		//public List<ProfileModel> Friends { get; set; }
		//public List<Message> Messages { get; set; }
    }
}
