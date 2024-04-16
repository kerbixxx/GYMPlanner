using GymPlanner.Application.Interfaces.Repositories.Chat;
using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatRepository _chatRepository;
        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        public IActionResult ChatRoom()
        {
            return View();
        }

    }
}
