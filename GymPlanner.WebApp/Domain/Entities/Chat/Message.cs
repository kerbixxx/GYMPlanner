using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Chat
{
    public class Message : BaseEntity
    {
        public int DialogId { get; set; }
        public Dialog? Dialog { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
