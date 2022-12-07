using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1ReadJson
{
    public class Test2
    {
        private readonly IOptionsSnapshot<Proxy> optionsSnapshot;
        public Test2(IOptionsSnapshot<Proxy> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }

        public void test2()
        {
            Console.WriteLine(optionsSnapshot.Value.address);
        }
    }
}
