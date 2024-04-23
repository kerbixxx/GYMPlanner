using GymPlanner.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Application.Models.Chat
{
    public class DialogDto
    {
        public int Id { get; set; }
        public string OtherUserName { get; set; }
    }
}
