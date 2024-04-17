using GymPlanner.Application.Interfaces.Repositories;
using GymPlanner.Application.Interfaces.Repositories.Chat;
using GymPlanner.Application.Models.Chat;
using GymPlanner.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPlanner.WebUI.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IUserRepository _userRepository;
        public ChatController(IMessageRepository messageRepository, IUserRepository userRepository, IDialogRepository dialogRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _dialogRepository = dialogRepository;
        }
        
        public async Task<IActionResult> ChatRoom()
        {
            var user = await _userRepository.FindByNameAsync(User.Identity.Name);
            var dialogs = await _dialogRepository.GetUserDialogsAsync(user.Id);
            var dialogDtos = new List<DialogDto>();
            foreach (var dialog in dialogs)
            {
                if(dialog.User.Email != User.Identity.Name)
                {
                    dialogDtos.Add(new DialogDto() { Id = dialog.Id, OtherUserName = dialog.User.Email });
                }
                else
                {
                    dialogDtos.Add(new DialogDto() { Id = dialog.Id, OtherUserName = dialog.OtherUser.Email });
                }
            }
            return View(dialogDtos);
        }
        
        public async Task<IActionResult> Messages(int dialogId)
        {
            var dialog = await _dialogRepository.GetAsync(dialogId);
            var messageDto = new MessagesDto()
            {
                DialogId = dialogId,
                Messages = dialog.Messages
            };
            if(dialog.User.Email == User.Identity.Name)
            {
                messageDto.SenderId = dialog.UserId;
                messageDto.SenderName = dialog.User.Email;
                messageDto.ReceiverId = dialog.OtherUserId;
                messageDto.ReceiverName = dialog.OtherUser.Email;
            }
            else
            {
                messageDto.SenderId = dialog.OtherUserId;
                messageDto.SenderName = dialog.OtherUser.Email;
                messageDto.ReceiverId = dialog.UserId;
                messageDto.ReceiverName = dialog.User.Email;
            }
            return View(messageDto);
        }

        public async Task<IActionResult> FindDialog(int userId)
        {
            var user = await _userRepository.FindByNameAsync(User.Identity.Name);
            var dialog = await _dialogRepository.GetDialogBetweenUsersAsync(user.Id,userId);
            if (dialog == null)
            {
                dialog = new Domain.Entities.Chat.Dialog
                {
                    UserId = userId,
                    OtherUserId = user.Id,
                };
                await _dialogRepository.AddAsync(dialog);
            }
            return RedirectToAction("Messages", new { Id =  dialog.Id});
        }
    }
}
