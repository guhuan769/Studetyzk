using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string title, string body)
        {
            Console.WriteLine($"通过Smtp发送邮件:{email} {title} {body} ");
            return Task.CompletedTask;
        }
    }
}
