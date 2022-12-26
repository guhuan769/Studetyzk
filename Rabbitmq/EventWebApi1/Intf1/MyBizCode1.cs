using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intf1
{
    public class MyBizCode1
    {
        private IEmailSender emailSender;
        private IMyDataProvider myDataProvider;

        public MyBizCode1(IEmailSender emailSender, IMyDataProvider myDataProvider)
        {
            this.emailSender = emailSender;
            this.myDataProvider = myDataProvider;
        }

        public async Task DoItAsync()
        {
            var items = myDataProvider.emailInfoSend();
            foreach (var item in items)
            {
                await emailSender.SendEmailAsync(item.Email, item.Title, item.Body);
                await Task.Delay(1000);
            }
        }
    }
}
