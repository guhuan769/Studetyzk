using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace DI会传染
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            //DI有什么好处呢 万一有一天配置更到数据库读取 那么此时只需要重新注册一个数据库相关的后台服务即可 
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddScoped<Controller>();
            serviceDescriptors.AddScoped<ILog, LogImpl>();
            serviceDescriptors.AddScoped<IStorage, StorageImpl>();
            //那么我只需要将此处的configimpl给注释重新注册DBConfig
            //serviceDescriptors.AddScoped<IConfig, ConfigImpl>();
            serviceDescriptors.AddScoped<IConfig, DBConfigImpl>();
            //所有的服务都要存放在BuildServiceProvider之前如  serviceDescriptors.AddScoped<IConfig,ConfigImpl>();
            using (var sp = serviceDescriptors.BuildServiceProvider())
            {
                var c = sp.GetRequiredService<Controller>();
                c.Test();
            }
        }
    }

    class Controller
    {
        private readonly ILog log;
        private readonly IStorage storage;
        public Controller(ILog log, IStorage storage)
        {
            this.log = log;
            this.storage = storage;
        }

        public void Test()
        {
            this.log.Log("开始上传");
            this.storage.Save("Test开始上传", "1.txt");
            this.log.Log("上传完毕");
        }
    }

    //日志服务
    interface ILog
    {
        public void Log(string message);
    }

    class LogImpl : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine($"日志:{message}");
        }
    }

    interface IConfig
    {
        public string GetValue(string msg);
    }

    class ConfigImpl : IConfig
    {
        public string GetValue(string msg)
        {
            return $"Hello{msg}";
        }
    }

    class DBConfigImpl : IConfig
    {
        public string GetValue(string msg)
        {
            Console.WriteLine($"从数据库读取配置{msg}");
            return $"从数据库读取配置{msg}";
        }
    }

    interface IStorage
    {
        public void Save(string content, string name);
    }

    class StorageImpl : IStorage
    {
        //DI优点降低模块之间的耦合 
        private readonly IConfig config;
        public StorageImpl(IConfig config)
        {
            this.config = config;
        }
        public void Save(string content, string name)
        {
            string service = config.GetValue("测试");
            Console.WriteLine($"向服务器{service}的文件名为{name}上传");
        }
    }

}