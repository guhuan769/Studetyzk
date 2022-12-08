using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.AOPDemo.Project.Common
{
    /// <summary>
    /// 面向对象 要扩展，都是靠抽象
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// log console----log txt-----log db
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(string msg)
        {
            Console.WriteLine($"控制台输出：{msg}");
        }

        public static void LogTxt(string msg)
        {
            Console.WriteLine($"文本输出：{msg}");
        }
    }

    public interface ILogHelper
    {
        void Log(string msg);
    }

    public class LogConsole : ILogHelper
    {
        public void Log(string msg)
        {
            Console.WriteLine($"控制台输出：{msg}");
        }
    }

    public class LogTxt : ILogHelper
    {
        public void Log(string msg)
        {
            Console.WriteLine($"文本输出：{msg}");
        }
    }
}
