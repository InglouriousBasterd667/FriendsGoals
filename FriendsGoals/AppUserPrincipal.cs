using System.Security.Claims;

namespace FriendsGoals
{
    public class AppUserPrincipal : ClaimsPrincipal
    {
        public AppUserPrincipal(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }
        public string Surname
        {
            get
            {
                return this.FindFirst(ClaimTypes.Surname).Value;
            }

        }
    }
}
