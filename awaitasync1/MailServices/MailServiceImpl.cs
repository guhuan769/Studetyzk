using LogServices;
using ConfigServices;

namespace MailServices
{
    public class MailServiceImpl : IMailService
    {
        private readonly ILogProvider logProvider;
        //private readonly IConfigService configService;
        private readonly IConfigReader configReader;

        //使用框架注入ILogProvider IConfigService
        public MailServiceImpl(ILogProvider logProvider, IConfigReader configReader)
        {
            this.logProvider = logProvider;
            this.configReader = configReader;
        }
        public void Send(string title, string to, string body)
        {
            this.logProvider.LogInfo("准备发邮件");

            string service = this.configReader.GetValue("service");
            string name = this.configReader.GetValue("UserName");
            string pwd = this.configReader.GetValue("PassWord");
            Console.WriteLine($"{service} {name} {pwd}");
            Console.WriteLine($"真发邮件啦{title},{to}");
            this.logProvider.LogInfo("邮件发送完毕");
        }
    }
}