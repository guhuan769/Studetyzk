using Microsoft.Extensions.DependencyInjection;

namespace IOC1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////ITestService testService = new TestServiceImp12();
            ////如果使用Ioc就不需要NEW了
            //ITestService testService = new TestServiceImp12();
            //testService.Name = "顾欢";
            //testService.SayHi();
            //Console.Read();

            ServiceCollection serviceDescriptors = new ServiceCollection();
            //直接注册实现类 此处与接口无关 方法1 AddTransient用AddTransient要谨慎 因为每次调用都是新的对象 太非内存了
            //serviceDescriptors.AddTransient<TestServiceImp1>();
            //单例 AddSingleton
            //方法2
            //serviceDescriptors.AddSingleton<TestServiceImp1>();
            //方法3
            serviceDescriptors.AddScoped<TestServiceImp1>();

            //由于ServiceProvider 继承了 IDisposable 接口所以得使用using（）{}语句
            //如果不适用using 那么此处会出现资源泄漏问题
            using (ServiceProvider sp = serviceDescriptors.BuildServiceProvider())//ServiceProvider == 服务定位器
            {
                /* 方法1 方法2
                TestServiceImp1 testServiceImp12 = sp.GetService<TestServiceImp1>();
                testServiceImp12.Name = "顾欢";
                testServiceImp12.SayHi();
                //那么我们再创建一个对象
                TestServiceImp1 testServiceImp1 = sp.GetService<TestServiceImp1>();
                //该方法比对2个对象是否是同一个对象是则返回true否则false
                //object.ReferenceEquals(testServiceImp1, testServiceImp12);
                Console.WriteLine(object.ReferenceEquals(testServiceImp1, testServiceImp12));
                //如果是false则证明每次调用GetService 都会返回一个新的对象 如果当我们的IOC ServiceCollection使用单例 AddSingleton 那么 
                //object.ReferenceEquals(testServiceImp1, testServiceImp12)因为单例是一个对象嘛 所以此处肯定是True
                //AddTransient使用该方法 就是Getservice 获取一个就是一个新实例 AddSingleton与 AddTransient 区别  AddTransient 瞬态的  AddSingleton单例的
                */
                //方法3
                using (IServiceScope scop = sp.CreateScope())
                {
                    //重 scop 的范围内的对象是同一个 那么出了此范围就算是相同类型那么也不属于同一对象
                    //在Scope中获取Scope相关的对象 scop。ServiceProvider而不是sp
                    TestServiceImp1 testServiceImp12 = scop.ServiceProvider.GetService<TestServiceImp1>();
                    testServiceImp12.Name = "顾欢";
                    testServiceImp12.SayHi();
                    //那么我们再创建一个对象
                    TestServiceImp1 testServiceImp1 = scop.ServiceProvider.GetService<TestServiceImp1>();
                    //该方法比对2个对象是否是同一个对象是则返回true否则false
                    //object.ReferenceEquals(testServiceImp1, testServiceImp12);
                    Console.WriteLine(object.ReferenceEquals(testServiceImp1, testServiceImp12));
                }

                using (IServiceScope scop2 = sp.CreateScope())
                {
                    //在Scope中获取Scope相关的对象 scop。ServiceProvider而不是sp
                    TestServiceImp1 testServiceImp12 = scop2.ServiceProvider.GetService<TestServiceImp1>();
                    testServiceImp12.Name = "顾欢";
                    testServiceImp12.SayHi();
                    //那么我们再创建一个对象
                    TestServiceImp1 testServiceImp1 = scop2.ServiceProvider.GetService<TestServiceImp1>();
                    //该方法比对2个对象是否是同一个对象是则返回true否则false
                    //object.ReferenceEquals(testServiceImp1, testServiceImp12);
                    Console.WriteLine(object.ReferenceEquals(testServiceImp1, testServiceImp12));
                }

            }
            //有人说了:我TMD 一个NEW 就能搞定的 你他妈搞这么多行 有什么意义？？？？？？？？？？？？？？？？？

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
}