using Microsoft.AspNetCore.Mvc;

namespace GymPlanner.WebUI.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult ChatRoom()
        {
            return View();
        }
    }
}
