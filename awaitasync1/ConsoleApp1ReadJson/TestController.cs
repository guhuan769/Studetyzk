using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1ReadJson
{
    /*
     该类注入到DI中
     */
    internal class TestController
    {
        //DI注入得都写成readonly IOptionsSnapshot 在一个范围内值不变
        public readonly IOptionsSnapshot<Config> optionsSnapshot;
        //如果这样写那么配置更改不会自动更新
        //public readonly Config optionsSnapshot;
        public TestController(IOptionsSnapshot<Config> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }

        public void Run() 
        {
            Console.WriteLine(optionsSnapshot.Value.age);
            Console.WriteLine("*************************");
            Console.WriteLine(optionsSnapshot.Value.proxy.address);
        }
    }
}
