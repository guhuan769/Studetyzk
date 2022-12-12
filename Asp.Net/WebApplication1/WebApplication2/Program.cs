using Microsoft.AspNetCore.Builder;
using �����м��;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
//https://localhost:7270/test ����test�ܵ� ���մ���˳��ִ��  
//ʵ����Ŀ��ǧ��Ҫ��  run֮ǰ������ݣ�����������м������ 
app.Map("/test", async (pipeBuilder) =>
{
    //�м��������Middleware��β ��Ϊ�¼���м�����Է��ʼ
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

    //�м��������Middleware��β
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
