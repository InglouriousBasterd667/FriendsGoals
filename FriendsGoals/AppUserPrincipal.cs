using System.Security.Claims;
using System.Collections.Generic;
using FriendsGoals.Models;

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
				return FindFirst(ClaimTypes.Name).Value;
			}
		}

		public string Surname
		{
			get
			{
				return FindFirst(ClaimTypes.Surname).Value;
			}

		}

		public string GivenName
		{
			get
			{
				return FindFirst(ClaimTypes.GivenName).Value;
			}

		}

		public string Phone
		{
			get
			{
				return FindFirst(ClaimTypes.MobilePhone).Value;
			}
		}

		public string Sex
		{
			get
			{
				return FindFirst(ClaimTypes.Gender).Value;
			}
		}
	}
}
