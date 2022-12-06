using Microsoft.Extensions.Configuration;

namespace ConsoleApp1ReadJson
{
    /*
        Microsoft.Extensions.Configuration
        Microsoft.Extensions.Configuration.Json 
        microsoft.extensions.configuration.binder.7.0.0.nupkg
        注意:一定要将Config.json文件 复制到输出目录 选择 如果较新则复制 不然就会出现无法读取
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("Config.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            string name = configurationRoot["name"];
            Console.WriteLine($"name={name}");
            //string add = configurationRoot.GetSection("proxy:address").Value;
            //Console.WriteLine($"add={add}");
            //读取json节点映射到实体
            //var get = configurationRoot.GetSection("proxy").Get<Proxy>();
            //直接获取根节点json 读取整个JSON 配置映射到实体
            var config = configurationRoot.Get<Config>();
        }
    }

    class Config
    {
        public string name { get; set; }
        public int age { get; set; }
        public Proxy proxy { get; set; }
    }

    class Proxy
    {
        public string address { get; set; }
    }
}