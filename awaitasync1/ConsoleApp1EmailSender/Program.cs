using ConfigServices;
using LogServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace ConsoleApp1EmailSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //注册的顺序无所谓 DI框架主要是用于降低耦合
            ServiceCollection services = new ServiceCollection();
           
            //services.AddScoped<IConfigService, EnvVarConfigService>();
            services.AddScoped<IConfigService, EnvVarConfigService>();
            //由于此处得初始化FilePath该属性那么此处就应该手动NEW
            //services.AddScoped(typeof(IConfigService), s => new IniFileConfigService() { FilePath = "Mail.ini" });
            services.AddScoped<IMailService, MailServiceImpl>();
            services.AddLayeredConfigRead();
            //services.AddScoped<ILogProvider, ConsoleLogProvider>();
            #region 扩展方法
            services.AddConsoleLog();
            services.AddIniFileConfig("Mail.ini");
            #endregion
            using (var sp = services.BuildServiceProvider())
            {
                //第一个根上的对象只能用ServiceLocator 
                var mainService = sp.GetRequiredService<IMailService>();
                mainService.Send("Hello", "769540542@qq.com", "Gu你好.");
            }

        }
    }
}