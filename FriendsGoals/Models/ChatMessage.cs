using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendsGoals.Models
{
	public class ChatMessage
	{
		[Key]
		public int messageID { get; set; }
		public AppUser User { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public string Text { get; set; } = "";
	}
}