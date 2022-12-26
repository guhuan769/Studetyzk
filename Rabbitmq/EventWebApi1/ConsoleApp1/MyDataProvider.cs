using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MyDataProvider : IMyDataProvider
    {
        public IEnumerable<EmailInfo> emailInfoSend()
        {
            string[] lines = File.ReadAllLines("e:/1.txt");
            foreach (var line in lines)
            {
                string[] s = line.Split("|");
                string email = s[0];
                string userName = s[1];
                string pwd = s[2];
                Console.WriteLine($" 发送邮件: {email} {userName} {pwd}");
                yield return new EmailInfo(email, userName, pwd);
            }
        }
    }
}
