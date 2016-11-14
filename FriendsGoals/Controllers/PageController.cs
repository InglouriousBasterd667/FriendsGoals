using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;

namespace FriendsGoals.Controllers
{
    public class PageController : Controller
    {
<<<<<<< HEAD
		public ActionResult MyPage() => View();
=======
        private readonly UserManager<AppUser> userManager;
		static ChatModel chat;

        public PageController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public PageController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult MyPage() => View();
>>>>>>> ff8e6b12298db7047abc14c927f6840d40170bd9

		public ActionResult Page(ProfileModel user) => View(user);

		public ActionResult MyFriends() => View();

		public ActionResult Friends(ProfileModel user) => View(user);

		public ActionResult Messages(string user, bool? logOn, bool? logOff, string chatMessage)
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
		}

		public void LogOff(ChatUser user)
		{
			chat.Users.Remove(user);
			chat.Messages.Add(new ChatMessage()
			{
				Text = user.Name + " покинул чат.",
				Date = DateTime.Now
			});
		}
	}
}