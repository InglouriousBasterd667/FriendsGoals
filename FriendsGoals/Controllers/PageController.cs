using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsGoals.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace FriendsGoals.Controllers
{
    public class PageController : AppController
    {
        private readonly UserManager<AppUser> userManager;
		private static ChatModel currentChat;

        public PageController() : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public PageController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
		}

        public ActionResult MyPage() => View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));

        public ActionResult Edit() => View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase upload)
        {
            AppDbContext db = new AppDbContext();
            AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
            if(TryUpdateModel(currentUser,"",new string[] {"Name","UserSurname", "Phone" })){
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        if (currentUser.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            // db.Files.Remove(currentUser.Files.First(f => f.FileType == FileType.Avatar));
                            //var file = currentUser.Files.First(f => f.FileType == FileType.Avatar);
                            //db.Entry(file).State = EntityState.Deleted;
                            //db.SaveChanges();
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        currentUser.Files = new List<File> { avatar };
                        userManager.Update(currentUser);
                        return RedirectToAction("MyPage");
                    }
                }
                catch(RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View();
        }

        public ActionResult ShowUser(string id) => View(userManager.Users.FirstOrDefault(x=> x.Id == id));

		public ActionResult MyFriends(string pressedElement)
		{
			if (pressedElement == "elem1")
			{
				ViewBag.Focused = pressedElement;
				ViewBag.PageToRender = "FriendsList";
			}
			else if (pressedElement == "elem2")
			{
				ViewBag.Focused = pressedElement;
				ViewBag.PageToRender = "FollowersList";
			}
			else if (pressedElement == "elem3")
			{
				ViewBag.Focused = pressedElement;
				ViewBag.PageToRender = "FollowingList";
			}

			return View(userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name));
		}

		public ActionResult FriendshipRequest(string id)
		{
			AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
			AppUser requestingFriend = userManager.Users.FirstOrDefault(x => x.Id == id);
			if (currentUser.Following == null) currentUser.Following = new List<AppUser>();
			if (requestingFriend.Followers == null) requestingFriend.Followers = new List<AppUser>();

			currentUser.Following.Add(requestingFriend);
			requestingFriend.Followers.Add(currentUser);

			userManager.Update(currentUser);
			userManager.Update(requestingFriend);

			return RedirectToAction("ShowUser", new { id = requestingFriend.Id });
		}

		public ActionResult AcceptRequest(string id)
		{
			AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
			AppUser newFriend = userManager.Users.FirstOrDefault(x => x.Id == id);
			if (currentUser.Friends == null) currentUser.Friends = new List<AppUser>();
			if (newFriend.Friends == null) newFriend.Friends = new List<AppUser>();

			currentUser.Followers.Remove(newFriend);
			currentUser.Friends.Add(newFriend);
			newFriend.Following.Remove(currentUser);
			newFriend.Friends.Add(currentUser);

			userManager.Update(currentUser);
			userManager.Update(newFriend);

			return RedirectToAction("MyFriends", new { pressedElement = "elem2" });
		}

		public ActionResult CancelRequest(string id)
		{
			AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
			AppUser requestedFriend = userManager.Users.FirstOrDefault(x => x.Id == id);

			currentUser.Following.Remove(requestedFriend);
			requestedFriend.Followers.Remove(currentUser);

			userManager.Update(currentUser);
			userManager.Update(requestedFriend);

			return RedirectToAction("MyFriends", new { pressedElement = "elem3" });
		}

		public ActionResult DeleteFriend(string id)
		{
			AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
			AppUser friend = userManager.Users.FirstOrDefault(x => x.Id == id);

			currentUser.Friends.Remove(friend);
			currentUser.Followers.Add(friend);
			friend.Friends.Remove(currentUser);
			friend.Following.Add(currentUser);

			userManager.Update(currentUser);
			userManager.Update(friend);

			return RedirectToAction("MyFriends", new { pressedElement = "elem1" });
		}


        public ActionResult AllUsers() => View(userManager.Users);

		public ActionResult Friends(string id) => View(userManager.Users.FirstOrDefault(x => x.Id == id));

		public ActionResult Messages()
		{
			AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
			AppUser friend = currentUser.Friends.First();
			currentUser.Dialogs = new List<ChatModel>();
			friend.Dialogs = new List<ChatModel>();

			ChatModel chat1 = new ChatModel();
			chat1.chatID = 1;
			chat1.ChatName = friend.Name + " " + friend.UserSurname;
			chat1.Users = new List<AppUser>();
			chat1.Users.Add(currentUser);
			chat1.Users.Add(friend);
			chat1.Messages = new List<ChatMessage>();
			chat1.Messages.Add(new ChatMessage { Date = DateTime.Now, messageID = 1, Text = "FirstMessage", User = currentUser });

			ChatModel chat2 = new ChatModel();
			chat1.chatID = 2;
			chat1.ChatName = currentUser.Name + " " + currentUser.UserSurname;
			chat1.Users = new List<AppUser>();
			chat1.Users.Add(friend);
			chat1.Users.Add(currentUser);
			chat1.Messages = new List<ChatMessage>();
			chat1.Messages.Add(new ChatMessage { Date = DateTime.Now, messageID = 1, Text = "FirstMessage", User = currentUser });

			currentUser.Dialogs.Add(chat1);
			friend.Dialogs.Add(chat2);

			userManager.Update(currentUser);
			userManager.Update(friend);

			return View(currentUser);
		}

		public ActionResult Chat(string userID, string message)
		{
			if (!Request.IsAjaxRequest())
			{
				AppUser interlocutor = userManager.Users.FirstOrDefault(x => x.Id == userID);
				AppUser currentUser = userManager.Users.FirstOrDefault(x => x.UserName == CurrentUser.Name);
				currentChat = currentUser.Dialogs.FirstOrDefault(x => x.Users.Last() == interlocutor);
				return View(currentChat);
			}
			else
			{
				return View(currentChat);
			}
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