using Intf1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MyDataProviderMock1 : IMyDataProvider
    {
        public IEnumerable<EmailInfo> emailInfoSend()
        {
            yield return new EmailInfo("1@qq.com","TitleGh","内容");
            yield return new EmailInfo("2@qq.com","TitleGh2","内容2");
            yield return new EmailInfo("3@qq.com","TitleGh3","内容3");
        }
    }
}
