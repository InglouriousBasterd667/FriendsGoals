using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsGoals.Models
{
	public class ChatMessage
	{
		public ChatUser User;
		public DateTime Date = DateTime.Now;
		public string Text = "";
	}
}