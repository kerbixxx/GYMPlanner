using GymPlanner.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymPlanner.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
