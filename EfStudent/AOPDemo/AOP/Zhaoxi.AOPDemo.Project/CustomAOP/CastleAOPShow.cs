using Castle.DynamicProxy;//autofac.AOP
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Zhaoxi.AOPDemo.Project.Common;

namespace Zhaoxi.AOPDemo.Project.CustomAOP
{
    public class CastleAOPShow
    {
        public static void Show()
        {
            #region CommonClass
            {
                //CommonClass commonClass = new CommonClass();
                //commonClass.MethodNoInterceptor();
                //commonClass.MethodInterceptor();
            }
            {
                //ProxyGenerator generator = new ProxyGenerator();
                //CustomInterceptor interceptor = new CustomInterceptor();//自定义拦截器
                //CommonClass test = generator.CreateClassProxy<CommonClass>(interceptor);//ClassProxy

                //Console.WriteLine("当前类型:{0},父类型:{1}", test.GetType(), test.GetType().BaseType);
                //Console.WriteLine();
                //test.MethodInterceptor();

                //Console.WriteLine();
                //test.MethodNoInterceptor();
                //Console.WriteLine();
                ////AOP是实现了---既不破坏封装，又能扩展功能
                ////但是这样岂不是依赖于一个CustomInterceptor--工作中有各种扩展：日志、异常、缓存、权限等等不同扩展，甚至还有不同组合，不同顺序，那该如何是好？
                ////希望像Filter一样，随便标记个特性，就能实现，怎么办？
                ////当下就是给了你一个支点，你就应该能翘起地球！程序已经有了AOP横切面(扩展点)，就可以堆叠其他技术去灵活完成--可以无限做文章的
            }
            #endregion

            #region InterfaceProxy
            {
                ProxyGenerator generator = new ProxyGenerator();
                CustomInterceptor interceptor = new CustomInterceptor();
                IDBHelper iDBHelper = new SqlServerDBHelper();
                var iDBHelperProxy = generator.CreateInterfaceProxyWithTarget<IDBHelper>(iDBHelper, interceptor);//InterfaceProxy

                Console.WriteLine("当前类型:{0},父类型:{1}", iDBHelperProxy.GetType(), iDBHelperProxy.GetType().BaseType);
                Console.WriteLine();
                iDBHelperProxy.Save(new UserInfo()
                {
                    Account = "Administrator",
                    Name = "Eleven",
                    Password = "888888"
                });

                Console.WriteLine();
                ////AOP是实现了---既不破坏封装，又能扩展功能
                ////但是这样岂不是依赖于一个CustomInterceptor--工作中有各种扩展：日志、异常、缓存、权限等等不同扩展，甚至还有不同组合，不同顺序，那该如何是好？
                ////希望像Filter一样，随便标记个特性，就能实现，怎么办？
                ////当下就是给了你一个支点，你就应该能翘起地球！程序已经有了AOP横切面(扩展点)，就可以堆叠其他技术去灵活完成--可以无限做文章的
                //就跟Filter差不多---靠特性---
            }
            #endregion

            #region CustomAttributeInterceptor
            //{
            //    ProxyGenerator generator = new ProxyGenerator();
            //    CustomAttributeInterceptor interceptor = new CustomAttributeInterceptor();
            //    IDBHelper iDBHelper = new SqlServerDBHelper();

            //    var iDBHelperProxy = generator.CreateInterfaceProxyWithTarget<IDBHelper>(iDBHelper, interceptor);

            //    Console.WriteLine("当前类型:{0},父类型:{1}", iDBHelperProxy.GetType(), iDBHelperProxy.GetType().BaseType);
            //    Console.WriteLine();

            //    iDBHelperProxy.Save(new UserInfo()
            //    {
            //        Account = "Administrator",
            //        Name = "Eleven",
            //        Password = "888888"
            //    });

            //    iDBHelperProxy.SaveNo(new UserInfo()
            //    {
            //        Account = "Administrator",
            //        Name = "Eleven",
            //        Password = "888888"
            //    });

            //    Console.WriteLine();
            //}
            #endregion
        }

        #region 0818
        public abstract class Before0818Attribute : Attribute
        {
            public abstract void Action();
        }
        public class LogBefore0818Attribute : Before0818Attribute
        {
            public override void Action()
            {
                Console.WriteLine("this is LogBefore0818Attribute Log");
            }
        }
        public class ParameterValidate0818Attribute : Before0818Attribute
        {
            public override void Action()
            {
                Console.WriteLine("this is ParameterValidate0818Attribute Log");
            }
        }
        public class Authorise0818Attribute : Before0818Attribute
        {
            public override void Action()
            {
                Console.WriteLine("this is Authorise0818Attribute Log");
            }
        }



        public class LogAfter0818Attribute : Attribute
        {

        }
        #endregion

        #region CommonClass
        public class CommonClass
        {
            public virtual void MethodInterceptor()
            {
                Console.WriteLine("This's CommonClass with Interceptor--virtual");
            }

            public void MethodNoInterceptor()
            {
                Console.WriteLine("This's CommonClass without Interceptor");
            }
        }
        #endregion

        #region InterfaceClass
        //DBHelper
        #endregion


        #region CustomInterceptor
        public class CustomInterceptor : StandardInterceptor
        {
            /// <summary>
            /// 调用前的拦截器
            /// </summary>
            /// <param name="invocation"></param>
            protected override void PreProceed(IInvocation invocation)
            {
                Console.WriteLine("调用前的拦截器，方法名是：{0}。", invocation.Method.Name);// 方法名   获取当前成员的名称。 
            }
            /// <summary>
            /// 拦截的方法返回时调用的拦截器
            /// </summary>
            /// <param name="invocation"></param>
            protected override void PerformProceed(IInvocation invocation)
            {
                Console.WriteLine("拦截的方法返回时调用的拦截器，方法名是：{0}。", invocation.Method.Name);
                //支点---横切面---不要写死---用特性+反射

                {
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    var method = invocation.Method;
                    //if (method.IsDefined(typeof(LogBefore0818Attribute), true))
                    //{
                    //    Console.WriteLine("this is LogBefore0818Attribute Log");
                    //}

                    if (method.IsDefined(typeof(Before0818Attribute), true))
                    {
                        foreach (Before0818Attribute attribute in method.GetCustomAttributes<Before0818Attribute>())
                        {
                            attribute.Action();
                        }
                    }

                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                }

                base.PerformProceed(invocation);//方法本身

                {
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    var method = invocation.Method;
                    if (method.IsDefined(typeof(LogAfter0818Attribute), true))
                    {
                        Console.WriteLine("this is LogAfter0818Attribute Log");
                    }
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                }
            }

            /// <summary>
            /// 调用后的拦截器
            /// </summary>
            /// <param name="invocation"></param>
            protected override void PostProceed(IInvocation invocation)
            {
                Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
            }
        }
        #endregion

        #region CustomAttributeInterceptor



        /// <summary>
        /// 父类
        /// </summary>
        [AttributeUsage(AttributeTargets.All, Inherited = true)]
        public abstract class BaseAOPAttribute : Attribute
        {
            public abstract void AOPAction(IInvocation invocation);

            //AOP前   AOP后  还搞点内部变量
            public abstract Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action);

            public int Order { get; set; } = 0;
        }

        public interface IAfterAOP
        {
            public void AfterAOPAction(IInvocation invocation);
        }
        public class LogAfterAttribute0612 : BaseAOPAttribute, IAfterAOP
        {
            //不能这样写---特性Attribute是在编译时确认的，不能直接注入---Filter可以？--靠的typeFilter--ServiceFilter--其实是把生成对象的地方，加了一层容器
            public LogAfterAttribute0612(IDBHelper dBHelper)
            {

            }



            /// <summary>
            /// 希望特性里面，不是直接执行逻辑，而是组装逻辑
            /// </summary>
            /// <param name="invocation"></param>
            /// <param name="action"></param>
            /// <returns></returns>
            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    action.Invoke(invocation);
                    Console.WriteLine($"This is LogAfterAttribute0612 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                };
            }

            public void AfterAOPAction(IInvocation invocation)
            {
                Console.WriteLine($"This is LogAfterAttribute0612 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            }

            public override void AOPAction(IInvocation invocation)
            {
            }
        }

        /// <summary>
        /// 标记个特性，就能AOP一组功能   在执行前写个日志
        /// </summary>
        [AttributeUsage(AttributeTargets.All, Inherited = true)]
        public class LogBeforeAttribute0609 : BaseAOPAttribute
        {
            public override void AOPAction(IInvocation invocation)
            {
                Console.WriteLine($"This is LogBeforeAttribute {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            }

            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    Console.WriteLine($"This is LogBeforeAttribute {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    action.Invoke(invocation);
                };
            }
        }

        public class ParameterValidateAttribute0612 : BaseAOPAttribute
        {
            public override void AOPAction(IInvocation invocation)
            {
                //暂时不检测
                Console.WriteLine("This is ParameterValidate {0}", string.Join("----", invocation.Arguments.Select(a => Newtonsoft.Json.JsonConvert.SerializeObject(a))));
            }

            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    //暂时不检测
                    Console.WriteLine("This is ParameterValidate {0}", string.Join("----", invocation.Arguments.Select(a => Newtonsoft.Json.JsonConvert.SerializeObject(a))));
                    action.Invoke(invocation);
                };
            }
        }

        public class IPCheckAttribute0612 : BaseAOPAttribute
        {
            public override void AOPAction(IInvocation invocation)
            {
                //暂时不检测
                Console.WriteLine("This is IPCheckAttribute {0}", "假定是ok的");
            }
            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    //暂时不检测
                    Console.WriteLine("This is IPCheckAttribute {0}", "假定是ok的");

                    action.Invoke(invocation);
                };
            }
        }

        public class MonitorAttribute0612 : BaseAOPAttribute
        {
            public override void AOPAction(IInvocation invocation)
            {

            }
            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();
                    action.Invoke(invocation);
                    stopwatch.Stop();
                    Console.WriteLine($"This is MonitorAttribute0612  {stopwatch.ElapsedMilliseconds}ms");
                };
            }
        }

        public class ExceptionAttribute0612 : BaseAOPAttribute
        {
            public override void AOPAction(IInvocation invocation)
            {

            }
            public override Action<IInvocation> PipeAction(IInvocation invocation, Action<IInvocation> action)
            {
                return invocation =>
                {
                    try
                    {
                        action.Invoke(invocation);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                };
            }
        }

        /// <summary>
        /// 通用的
        /// </summary>
        public class CustomAttributeInterceptor : StandardInterceptor
        {
            protected override void PerformProceed(IInvocation invocation)
            {
                var method = invocation.Method;

                #region 未封装
                //if (method.IsDefined(typeof(LogBeforeAttribute0609), true))
                //{
                //    var attribute = method.GetCustomAttribute<LogBeforeAttribute0609>()!;
                //    attribute.AOPAction(invocation);

                //    //Console.WriteLine($"This is LogBeforeAttribute {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //}
                //if (method.IsDefined(typeof(ParameterValidateAttribute0612), true))
                //{
                //    var attribute = method.GetCustomAttribute<ParameterValidateAttribute0612>()!;
                //    attribute.AOPAction(invocation);

                //    ////暂时不检测
                //    //Console.WriteLine("This is ParameterValidate {0}", string.Join("----", invocation.Arguments.Select(a => Newtonsoft.Json.JsonConvert.SerializeObject(a))));
                //}
                #endregion

                #region 未流程组装
                //if (method.IsDefined(typeof(BaseAOPAttribute), true))
                //{
                //    var attributeArray = method.GetCustomAttributes<BaseAOPAttribute>();
                //    foreach (var attribute in attributeArray)
                //    {
                //        attribute.AOPAction(invocation);
                //    }
                //}

                //base.PerformProceed(invocation);//real 业务逻辑

                //if (method.IsDefined(typeof(BaseAOPAttribute), true))
                //{
                //    var attributeArray = method.GetCustomAttributes<BaseAOPAttribute>().Where(a => a is IAfterAOP);//
                //    foreach (var attribute in attributeArray)
                //    {
                //        ((IAfterAOP)attribute).AfterAOPAction(invocation);
                //    }
                //}
                ////代码重复的有点多
                #endregion

                #region 流程组装，后续执行

                Action<IInvocation> action = i => base.PerformProceed(i);//real 业务逻辑

                if (method.IsDefined(typeof(BaseAOPAttribute), true))
                {
                    var attributeArray = method.GetCustomAttributes<BaseAOPAttribute>();
                    foreach (var attribute in attributeArray.OrderBy(a => a.Order))
                    {
                        action = attribute.PipeAction(invocation, action);
                    }
                }
                action.Invoke(invocation);
                #endregion

                //顺序：方法前 方法后---多个前之间的顺序问题--Order排序---方法前和方法后的是不一样的顺序
            }

            List<int> ints = new List<int>(5);//4个 5个   初始化完了，记得TrimExcess

        }

        #endregion
    }
}
