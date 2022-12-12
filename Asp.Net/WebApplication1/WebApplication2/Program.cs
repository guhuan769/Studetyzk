using Microsoft.AspNetCore.Builder;
using 构建中间件;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//https://localhost:7270/test 进入test管道 按照代码顺序执行  
//实际项目中千万不要在  run之前输出内容，这样会造成中间件混乱 
app.Map("/test", async (pipeBuilder) =>
{
    //中间件命名以Middleware结尾 因为事检查中间件所以放最开始
    pipeBuilder.UseMiddleware<CheckMiddleWare>();

    pipeBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("1 end <br/>");
    });

    pipeBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("2 start <br/>");
        await next.Invoke();
        await context.Response.WriteAsync("2 end <br/>");
    });

    //中间件命名以Middleware结尾
    pipeBuilder.UseMiddleware<Test1MiddleWare>();

    pipeBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("run <br/>");
        dynamic? obj = context.Items["BodyJson"];
        if (obj != null)
        {
            await context.Response.WriteAsync($"{obj} + <br/>");
        }
    });
});
app.Run();
