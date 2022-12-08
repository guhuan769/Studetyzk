using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zhaoxi.AOPDemo.Project.CustomAOP
{
    /// <summary>
    /// 为ZhaoxiContainer做个AOP扩展
    /// </summary>
    public static class CustomAOPExtend
    {
        public static object AOP(this object t, Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            IOCInterceptor interceptor = new IOCInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }
    }

    #region attribute Interceptor
    public abstract class BaseInterceptorAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation, Action action);
    }
    //[AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class LogBeforeAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogBeforeAttribute1 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //去执行真实逻辑
                action.Invoke();
                //写个日志---参数检查--能做的事儿已经很多了
                Console.WriteLine($"This is LogBeforeAttribute2 {invocation.Method.Name} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is LogAfterAttribute1  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                //去执行真实逻辑
                action.Invoke();
                Console.WriteLine($"This is LogAfterAttribute2  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            };
        }
    }

    public class MonitorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                Console.WriteLine("This is MonitorAttribute 1");
                stopwatch.Start();

                //去执行真实逻辑
                action.Invoke();
                //想做个缓存拦截，觉得自己能搞定刷个1

                stopwatch.Stop();
                Console.WriteLine($"本次方法花费时间{stopwatch.ElapsedMilliseconds}ms");
                Console.WriteLine("This is MonitorAttribute 2");
            };
        }
    }

    public class LoginAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine("This is LoginAttribute 1");
                action.Invoke();
                Console.WriteLine("This is LoginAttribute 2");
            };
        }
    }
    #endregion

    /// <summary>
    /// 切面逻辑--写死，不能满足灵活需求
    /// </summary>
    public class IOCInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            //Console.WriteLine("调用前的拦截器123，方法名是：{0}。", invocation.Method.Name);// 方法名   获取当前成员的名称。 
        }
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            var method = invocation.Method;
            Action action = () => base.PerformProceed(invocation); //

            //顺序控制
            if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
            {
                //想注入 就容器实例---
                foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray().Reverse())
                {
                    action = attribute.Do(invocation, action);
                }//ASP.NET CORE 俄罗斯套娃记得刷个1
            }
            //那就说明前面不能执行具体动作--前面只能是组装动作---配置管道模型---委托

            //base.PerformProceed(invocation);//就是真实动作
            action.Invoke();

            //else
            //{
            //    base.PerformProceed(invocation);
            //}
            //1 能解决哪些方法不用AOP的问题
            //2 可以把切面逻辑转移到特性里面去

            //base.PerformProceed(invocation);//就是真实动作
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            //Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
        }
    }
}
