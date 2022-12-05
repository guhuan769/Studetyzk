using Microsoft.Extensions.DependencyInjection;

namespace ICO1NEWMAIN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            //参数1指的是服务的类型 //第二个参数指向实现类型
            //serviceDescriptors.AddScoped<ITestService, TestServiceImp1>();
            //serviceDescriptors.AddScoped(typeof(ITestService), typeof(TestServiceImp1));
            
            //手动New TestServiceImp1
            serviceDescriptors.AddSingleton(typeof(ITestService), new TestServiceImp1());
            serviceDescriptors.AddSingleton(typeof(ITestService), new TestServiceImp12());
            using (ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider())
            {
                //GetService 如果找不到服务就返回null
                //ITestService testService = serviceProvider.GetService<ITestService>();
                //GetRequiredService 如果找不到服务不会返回null 直接抛异常而不会返回null
                ITestService testService = serviceProvider.GetRequiredService<ITestService>();
                testService.Name = "Tom";
                testService.SayHi();

                //可以返回多个服务
                IEnumerable<ITestService> services = serviceProvider.GetServices<ITestService>();
                //打印对象是什么
                Console.WriteLine(services.GetType());
            }
        }


    }

    public interface ITestService
    {
        public string Name { get; set; }
        public void SayHi();
    }

    public class TestServiceImp1 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"你好,我是{Name}");
        }
    }
    public class TestServiceImp12 : ITestService
    {
        public string Name { get; set; }

        public void SayHi()
        {
            Console.WriteLine($"你好,我是{Name}");
        }
    }
}