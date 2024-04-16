using GymPlanner.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Chat
{
    public class Chat : BaseEntity
    {
        public int UserId { get; set; }
        public int ReceiverId { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
