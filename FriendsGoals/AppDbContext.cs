using FriendsGoals.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FriendsGoals
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<File> Files { get; set; }

        public AppDbContext() : base("AppDatabase")
        {
        }

    }
}