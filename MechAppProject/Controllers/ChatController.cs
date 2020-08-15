using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index(int? currentChat, int? workshopId)
        {
            var userSession = Session["Login"] as SessionModel;
            var workshopSession = Session["LoginWorkshop"] as SessionModel;
            var model = new ChatModel();

            if (userSession != null || workshopSession != null)
            {
                if (workshopId > 0)
                {
                    using (var db = new MechAppProjectEntities())
                    {
                        var workshop = db.Workshops.First(x => x.WorkshopId == workshopId);

                        model.TemporaryChat = true;
                        model.CurrentWorkshopName = workshop.WorkshopName;
                        model.Messages = new List<ChatMessageModel>();
                        model.CurrentWorkshopId = workshop.WorkshopId;
                        model.CurrentCustomerId = userSession.UserId;
                        model.CurrentCustomerEmail = db.Customers.First(x => x.CustomerId == userSession.UserId).Email;
                        model.CurrentWorkshopEmail = workshop.Email;
                    }
                }

                if (currentChat > 0)
                {
                    using (var db = new MechAppProjectEntities())
                    {
                        var chat = db.Chats.First(x => x.ChatId == currentChat);

                        model.CurrentChatId = currentChat.Value;
                        model.CurrentWorkshopName = chat.Workshop.WorkshopName;
                        model.Messages = DeserializeChatMessages(chat.Message);
                        model.CurrentWorkshopId = chat.WorkshopId;
                        model.CurrentCustomerId = chat.CustomerId;
                        model.CurrentCustomerEmail = chat.Customer.Email;
                        model.CurrentWorkshopEmail = chat.Workshop.Email;
                    }
                }

                using (var db = new MechAppProjectEntities())
                {
                    var chats = new List<Chat>();

                    if (userSession != null)
                    {
                        model.IsCustomer = true;
                        chats = db.Chats.Where(x => x.CustomerId == userSession.UserId).ToList();
                    }

                    if (workshopSession != null)
                    {
                        chats = db.Chats.Where(x => x.WorkshopId == workshopSession.WorkshopId).ToList();
                    }

                    foreach (var chat in chats)
                    {
                        model.ChatRooms.Add(new ChatRoomModel()
                        {
                            ChatId = chat.ChatId,
                            WorkshopName = chat.Workshop.WorkshopName,
                            CustomerName = chat.Customer.Name
                        });
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessage(ChatModel model)
        {
            int currentChatId = 0;
            var newChat = new Chat();
            var userSession = Session["Login"] as SessionModel;
            var workshopSession = Session["LoginWorkshop"] as SessionModel;

            if (userSession != null || workshopSession != null)
            {
                var sentBy = string.Empty;
                var sentTo = string.Empty;

                if (userSession != null)
                {
                    sentBy = model.CurrentCustomerEmail;
                    sentTo = model.CurrentWorkshopEmail;
                }

                if (workshopSession != null)
                {
                    sentBy = model.CurrentWorkshopEmail;
                    sentTo = model.CurrentCustomerEmail;
                }

                using (var db = new MechAppProjectEntities())
                {
                    if (model.CurrentChatId > 0)
                    {
                        var chat = db.Chats.FirstOrDefault(x => x.ChatId == model.CurrentChatId);

                        if (chat != null)
                        {
                            var newMessage = SerializeChatMessage(new ChatMessageModel() { Message = model.CurrentMessage, Date = DateTime.Now, SentBy = sentBy, SentTo = sentTo });
                            chat.Message += newMessage;
                        }

                        currentChatId = chat.ChatId;
                    }
                    else
                    {
                        newChat = new Chat()
                        {
                            CustomerId = model.CurrentCustomerId,
                            WorkshopId = model.CurrentWorkshopId,
                            Message = SerializeChatMessage(new ChatMessageModel() { Message = model.CurrentMessage, Date = DateTime.Now, SentBy = sentBy, SentTo = sentTo })
                        };

                        db.Chats.Add(newChat);
                    }

                    db.SaveChanges();

                    if (model.CurrentChatId <= 0)
                    {
                        currentChatId = newChat.ChatId;
                    }
                }
            }

            return RedirectToAction("Index", new { currentChat = currentChatId });
        }

        public string SerializeChatMessage(ChatMessageModel message)
        {
            return message.Message + ";" + message.Date + ";" + message.SentBy + ";" + message.SentTo + "|";
        }

        public List<ChatMessageModel> DeserializeChatMessages(string messages)
        {
            var chatMessageList = new List<ChatMessageModel>();
            var messagesArray = messages.Split('|');
            messagesArray = messagesArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            foreach (var message in messagesArray)
            {
                var messageData = message.Split(';');

                chatMessageList.Add(new ChatMessageModel()
                {
                    Message = messageData[0],
                    Date = DateTime.Parse(messageData[1]),
                    SentBy = messageData[2],
                    SentTo = messageData[3]
                });
            }

            return chatMessageList;
        }
    }
}