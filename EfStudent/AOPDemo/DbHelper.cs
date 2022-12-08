using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class DbHelper
    {
        List<object> list = new List<object>();
        public void Before()
        {
            Console.WriteLine("Before Save");
        }

        //该方法属于代理模式 静态代理啊  如果要扩展就在Before After 方法中iuoz 充分得达到了 既不破坏封装 却扩展了功能
        public int Save(object o)
        {
            Before();
            list.Add(o);
            After();
            return list.Count;
        }

        public void After()
        {
            Console.WriteLine("After Save");
        }
    }
}
