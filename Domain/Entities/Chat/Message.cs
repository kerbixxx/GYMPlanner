using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Domain.Entities.Chat
{
    public class Message : BaseEntity
    {
        public string UserIdFrom { get; set; }
        public string UserIdTo { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
