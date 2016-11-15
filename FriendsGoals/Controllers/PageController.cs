using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;
using Microsoft.AspNet.Identity;

namespace FriendsGoals.Controllers
{
    public class PageController : AppController
    {
        private readonly UserManager<AppUser> userManager;

        public PageController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public PageController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
		}

        public ActionResult MyPage() => View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));

        public ActionResult ShowUser(string id) => View(userManager.Users.FirstOrDefault(x=> x.Id == id));

		public ActionResult MyFriends() => View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));

        public ActionResult AddFriend(string id)
        {
            //AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
            AppUser friend = userManager.Users.FirstOrDefault(x => x.Id == id);
            //if (currentUser.Friends == null) currentUser.Friends = new List<AppUser>();
            //currentUser.Friends.Add(friend);
            if (userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Friends == null)
                userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Friends = new List<AppUser>();
            userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Friends.Add(friend);
            return RedirectToAction("MyFriends");
        }

		public ActionResult AllUsers() => View(userManager.Users);

		public ActionResult Friends(ProfileModel user) => View(user);

		public ActionResult Messages()
		{
			userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Dialogs = new List<ChatModel>();

			ChatModel chat1 = new ChatModel();
			chat1.chatID = 1;
			chat1.Users = new List<AppUser>();
			chat1.Users.Add(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));
			chat1.Messages = new List<ChatMessage>();
			chat1.Messages.Add(new ChatMessage { Date = DateTime.Now, messageID = 1, Text = "FirstMessage", User = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name) });
			userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Dialogs.Add(chat1);

			ChatModel chat2 = new ChatModel();
			chat2.chatID = 2;
			chat2.Users = new List<AppUser>();
			chat2.Users.Add(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));
			chat2.Messages = new List<ChatMessage>();
			chat2.Messages.Add(new ChatMessage { Date = DateTime.Now, messageID = 2, Text = "SecondMessage", User = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name) });
			userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Dialogs.Add(chat2);

			ChatModel chat3 = new ChatModel();
			chat3.chatID = 3;
			chat3.Users = new List<AppUser>();
			chat3.Users.Add(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));
			chat3.Messages = new List<ChatMessage>();
			chat3.Messages.Add(new ChatMessage { Date = DateTime.Now, messageID = 3, Text = "ThirdMessage", User = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name) });
			userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name).Dialogs.Add(chat3);

			return View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));
		}

		/*public ActionResult Messages(string user, bool? logOn, bool? logOff, string chatMessage)
		{
			try
			{
				if (chat == null) chat = new ChatModel();

				if (!Request.IsAjaxRequest()) return View(chat);
				else if (logOn != null && (bool)logOn)
				{
					//проверяем, существует ли уже такой пользователь
					if (chat.Users.FirstOrDefault(u => u.Name == user) != null)
					{
						throw new Exception("Пользователь с таким ником уже существует");
					}
					else if (chat.Users.Count > 10)
					{
						throw new Exception("Чат заполнен");
					}
					else
					{
						// добавляем в список нового пользователя
						chat.Users.Add(new ChatUser()
						{
							Name = user,
							LoginTime = DateTime.Now,
							LastPing = DateTime.Now
						});

						// добавляем в список сообщений сообщение о новом пользователе
						chat.Messages.Add(new ChatMessage()
						{
							Text = user + " вошел в чат",
							Date = DateTime.Now
						});
					}

					return PartialView("ChatRoom", chat);
				}
				else if (logOff != null && (bool)logOff)
				{
					LogOff(chat.Users.FirstOrDefault(u => u.Name == user));
					return PartialView("ChatRoom", chat);
				}
				else
				{
					ChatUser currentUser = chat.Users.FirstOrDefault(u => u.Name == user);

					//для каждлого пользователя запоминаем воемя последнего обновления
					currentUser.LastPing = DateTime.Now;

					// удаляем неавтивных пользователей, если время простоя больше 15 сек
					List<ChatUser> toRemove = new List<ChatUser>();
					foreach (Models.ChatUser usr in chat.Users)
					{
						TimeSpan span = DateTime.Now - usr.LastPing;
						if (span.TotalSeconds > 15)
							toRemove.Add(usr);
					}
					foreach (ChatUser u in toRemove)
					{
						LogOff(u);
					}

					// добавляем в список сообщений новое сообщение
					if (!string.IsNullOrEmpty(chatMessage))
					{
						chat.Messages.Add(new ChatMessage()
						{
							User = currentUser,
							Text = chatMessage,
							Date = DateTime.Now
						});
					}

					return PartialView("History", chat);
				}
			}
			catch (Exception ex)
			{
				//в случае ошибки посылаем статусный код 500
				Response.StatusCode = 500;
				return Content(ex.Message);
			}
		}*/

		/*public void LogOff(ChatUser user)
		{
			chat.Users.Remove(user);
			chat.Messages.Add(new ChatMessage()
			{
				Text = user.Name + " покинул чат.",
				Date = DateTime.Now
			});
		}*/
	}
}