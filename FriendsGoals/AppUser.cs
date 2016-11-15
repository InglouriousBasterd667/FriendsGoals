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

        public virtual ICollection<AppUser> Friends { get; set; }
        public virtual ICollection<AppUser> Followers { get; set; }
        public virtual ICollection<AppUser> Following { get; set; }
		public virtual ICollection<ChatModel> Dialogs { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
