using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intf1
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string title, string body);
    }
}