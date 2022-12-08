using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.AOPDemo.Project.CustomAOP;
using static Zhaoxi.AOPDemo.Project.CustomAOP.CastleAOPShow;

namespace Zhaoxi.AOPDemo.Project.Common
{
    /// <summary>
    /// 内部修改类---因为封装，上端暂时不用改动
    /// 假如需要切换---总不能再去if else---抽象了！
    /// </summary>
    public interface IDBHelper
    {
        //[LogBeforeAttribute0609(Order = 1000)]
        //[IPCheckAttribute0612(Order = 13)]
        ////[LogAfterAttribute0612(Order = 100)]
        //[MonitorAttribute0612]

        [LogBefore0818Attribute]
        [LogAfter0818Attribute]
        [Authorise0818Attribute]
        [ParameterValidate0818]
        int Save(UserInfo userInfo);

        [ParameterValidateAttribute0612]
        public int SaveNo(UserInfo userInfo)
        {
            Console.WriteLine("This is IDBHelper SaveNo");
            return 1;
        }
    }

    /// <summary>
    /// 内部修改类---因为封装，上端暂时不用改动
    /// 假如需要切换---总不能再去if else---抽象了！
    /// </summary>
    public class SqlServerDBHelper : IDBHelper
    {

        public int Save(UserInfo userInfo)
        {
            //try
            //{
            Console.WriteLine("数据入SqlServer");
            Thread.Sleep(1000);
            return default;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}
        }
    }

    /// <summary>
    /// 要增加逻辑
    /// 1  内部修改类---因为封装，上端暂时不用改动
    /// 2  使用工厂，整个对象替换---OOP的扩展性全靠这个
    /// </summary>
    public class MySQLDBHelper : IDBHelper
    {
        private ILogHelper helper = new LogConsole();

        //[LogBeforeAttribute0609]
        public int Save(UserInfo userInfo)
        {
            Console.WriteLine("数据入MySQL");
            return default;
        }
    }

    /// <summary>
    /// 替换类
    /// </summary>
    public class MysqlDBHelperV2 : IDBHelper
    {
        private ILogHelper helper = new LogConsole();

        public int Save(UserInfo userInfo)
        {
            helper.Log("Prepare Save");
            try
            {
                Console.WriteLine("数据入SQLServer");
                Console.WriteLine("数据入MySQL");
                LogHelper.LogTxt("数据入MySQL");

                helper.Log("数据入MySQL");

                helper.Log("After Save");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
