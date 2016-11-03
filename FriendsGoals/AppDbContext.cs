using Microsoft.AspNet.Identity.EntityFramework;


namespace FriendsGoals
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("AppDatabase")
        {
        }
    }
}