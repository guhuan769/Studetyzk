using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.AOPDemo.Project
{
    /// <summary>
    /// POP面向过程编程演练
    /// </summary>
    public class POPShow
    {
        /// <summary>
        /// 模拟用户注册,3个步骤
        /// 面向过程式编程
        /// 线性思维的体现
        /// 不能应对变化
        /// </summary>
        public static void Show()
        {
            Console.WriteLine("用户注册，提交信息");
            Console.WriteLine("1 参数检查");
            Console.WriteLine("2 数据入库");
            Console.WriteLine("3 记录日志");
        }
    }
}
