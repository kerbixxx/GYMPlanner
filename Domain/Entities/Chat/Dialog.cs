using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Chat
{
    public class Dialog : BaseEntity
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public string OtherUserName { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
