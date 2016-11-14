using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsGoals.Models
{
	public class ChatModel
	{
		public ICollection<ChatUser> Users;
		public ICollection<ChatMessage> Messages;

		public ChatModel()
		{
			Users = new List<ChatUser>();
			Messages = new List<ChatMessage>();

			Messages.Add(new ChatMessage() { Text = "Чат запущен " + DateTime.Now });
		}
	}
}