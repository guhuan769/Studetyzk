using Dynamic.Json;

namespace 构建中间件
{
    public class CheckMiddleWare
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 一般传入的next 都保存起来
        /// </summary>
        /// <param name="next"></param>
        public CheckMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //https://localhost:7270/test?password=123 
            string pwd = context.Request.Query["password"];
            if (pwd.Equals("123"))
            {
                if (context.Request.HasJsonContentType())
                {
                    var stream = context.Request.BodyReader.AsStream();
                    //system.text.json 目前(.net6)不支持吧json反序列化为dynamic类型所以此处就是用Dynamic.Json
                    dynamic obj = await DJson.ParseAsync(stream);
                    context.Items["BodyJson"] = obj;
                }
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
