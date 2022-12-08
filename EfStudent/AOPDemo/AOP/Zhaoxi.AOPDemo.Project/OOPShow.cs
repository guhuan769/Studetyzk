using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.AOPDemo.Project.Common;

namespace Zhaoxi.AOPDemo.Project
{
    /// <summary>
    /// OOP面向对象编程演练
    /// </summary>
    public class OOPShow
    {
        /// <summary>
        /// OOP面向对象编程
        /// 1 考虑有几个对象
        /// 2 封装屏蔽细节
        /// 3 还可以继承完成代码复用
        /// 4 支持多态，尤其是面向抽象编程
        /// 
        /// 内部升级---面向对象的内部还是面向过程的
        /// </summary>
        public static void Show()
        {
            Console.WriteLine("用户注册，提交信息");

            UserInfo userInfo = new UserInfo()
            {
                Account = "Administrator",
                Name = "Eleven",
                Password = "888888"
            };

            //还可以把校验也封装
            if (userInfo.Account == null)
            {
                Console.WriteLine("注册失败");
                return;
            }

            //MySQLDBHelper dbHelper = new MySQLDBHelper();
            //封装好了---但是有需求变更，增加日志、异常处理、缓存---只有2种方式：
            //1  修改方法---破坏封装，影响稳定性，违背开闭原则
            //2  面向抽象---来个新的类实现，然后替换一下，类似于IOC---大功告成---
            //但这就是OOP的局限性！只能以类为单位来做拓展，因为类是OOP的组成原子，不容修改--但是扩展需求，就只能抽象+替换类（90%设计模式）----
            //有没有这么一个玩法：既不破坏类的封装(也不替换类)，但又能扩展功能，有！这个就叫AOP
            //AOP是OOP的补充，用来扩展OOP对象通用功能(非业务功能)---


            IDBHelper dbHelper = SimpleFactory.CreateDBHelper();//当然也可以IOC
            dbHelper.Save(userInfo);

            LogHelper.Log("用户注册成功");
        }
    }

    public class SimpleFactory
    {
        public static IDBHelper CreateDBHelper()
        {
            //return new MySQLDBHelper();
            return new MysqlDBHelperV2();
            //return new SqlServerDBHelper();
        }
    }
}
