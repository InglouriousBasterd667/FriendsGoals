using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendsGoals.Models
{
	public class ChatModel
	{
		[Key]
		public int chatID { get; set; }
		public ICollection<AppUser> Users { get; set; }
		public ICollection<ChatMessage> Messages { get; set; }
	}
}