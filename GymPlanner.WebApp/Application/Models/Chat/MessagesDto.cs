using GymPlanner.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Chat
{
    public class MessagesDto
    {
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public int DialogId { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
