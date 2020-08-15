using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class ChatModel
    {
        public bool TemporaryChat { get; set; }
        public int CurrentWorkshopId { get; set; }
        public int CurrentCustomerId { get; set; }
        public string CurrentCustomerEmail { get; set; }
        public string CurrentWorkshopEmail { get; set; }
        public int CurrentChatId { get; set; }
        public string CurrentWorkshopName { get; set; }
        public string CurrentMessage { get; set; }
        public bool IsCustomer { get; set; }
        public List<ChatRoomModel> ChatRooms { get; set; }
        public List<ChatMessageModel> Messages { get; set; }

        public ChatModel()
        {
            ChatRooms = new List<ChatRoomModel>();
            Messages = new List<ChatMessageModel>();
        }
    }

    public class ChatRoomModel
    {
        public int ChatId { get; set; }
        public string WorkshopName { get; set; }
        public string CustomerName { get; set; }

    }

    public class ChatMessageModel
    {
        public string Message { get; set; }
        public DateTime Date {get; set; }
        public string SentBy { get; set; }
        public string SentTo { get; set; }
    }
}