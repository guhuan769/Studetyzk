using System.Net.Mime;

namespace 构建中间件
{
    /*自定义中间件 类无需继承任何接口以及类*/
    public class Test1MiddleWare
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 一般传入的next 都保存起来
        /// </summary>
        /// <param name="next"></param>
        public Test1MiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Test1MiddleWare start<br/>");
            await next.Invoke(context);
            await context.Response.WriteAsync("Test1MiddleWare end<br/>");
            //string pwd = context.Request.Query["password"];
            //if (pwd.Equals("123"))
            //{
            //    //if(context.Request.HasJsonContentType)
            //}
        }
    }
}
