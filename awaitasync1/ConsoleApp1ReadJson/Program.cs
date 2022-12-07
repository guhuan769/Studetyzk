using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1ReadJson
{
    /*
        Microsoft.Extensions.Configuration
        Microsoft.Extensions.Configuration.Json 
        microsoft.extensions.configuration.binder.7.0.0.nupkg
        microsoft.extensions.options.7.0.0.nupkg
        安装 该包
        microsoft.extensions.configuration.commandline.7.0.0.nupkg
        注意:一定要将Config.json文件 复制到输出目录 选择 如果较新则复制 不然就会出现无法读取


     */
    internal class Program
    {
        static void Main(string[] args)
        {
            //开始注入
            ServiceCollection services = new ServiceCollection();
            //DI注入顺序无所谓 将 TestController注入到di框架中
            services.AddScoped<TestController>();
            //注册TEST2
            services.AddScoped<Test2>();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.AddJsonFile("Config.json", optional: true, reloadOnChange: true);
            //如果不用AddJsonFile 那么就只有使用 AddCommandLine
            configurationBuilder.AddCommandLine(args);
            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            //吧Config对象绑定到根节点上去
            services.AddOptions().Configure<Config>(e => configurationRoot.Bind(e));
            //GetSection 只读 Proxy 这种写法也可以称为链式编程 
            services.AddOptions().Configure<Config>(e => configurationRoot.Bind(e)).
                Configure<Proxy>(e => configurationRoot.GetSection("Proxy").Bind(e));

            //调用方法
            using (var sp = services.BuildServiceProvider())
            {
                //这样写 动态更改配置是不会变得
                //while (true)
                //{
                //    //泛型类型是注册类型
                //    var c = sp.GetRequiredService<TestController>();
                //    c.Run();
                //    var t = sp.GetRequiredService<Test2>();
                //    t.test2();
                //    Console.WriteLine("点击任意键继续");
                //    Console.ReadKey();
                //}


                while (true)
                {
                    //所以需要使用scope 可以动态读取配置文件比如 系统根目录下得配置文件更改就会立即获取最新值 
                    //每一次都需要创建一个新得scope 就会立即生效
                    using (var scope = sp.CreateScope())
                    {

                        var c = scope.ServiceProvider.GetRequiredService<TestController>();
                        c.Run();
                        Console.WriteLine("更改地址");
                        Console.ReadKey();
                        c.Run();
                        var t = scope.ServiceProvider.GetRequiredService<Test2>();
                        t.test2();
                        Console.WriteLine("点击任意键继续");
                        Console.ReadKey();
                    }
                }
            }
            string name = configurationRoot["name"];
            Console.WriteLine($"name={name}");
            //string add = configurationRoot.GetSection("proxy:address").Value;
            //Console.WriteLine($"add={add}");
            //读取json节点映射到实体
            //var get = configurationRoot.GetSection("proxy").Get<Proxy>();
            //直接获取根节点json 读取整个JSON 配置映射到实体
            var config = configurationRoot.Get<Config>();

            //如果通过依赖注入来读配置会更加的优美 
        }
    }

    class Config
    {
        public string name { get; set; }
        public int age { get; set; }
        public Proxy proxy { get; set; }
    }

    public class Proxy
    {
        public string address { get; set; }

        public string port { get; set; }

        public int[] id { get; set; }
    }
}