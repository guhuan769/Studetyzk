using ClassLibrary1;
using WebAPI;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

//这句话讲controller注入到DI中
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region 在var app = builder.Build();之前配置CORS
builder.Services.AddCors(opt => {
    opt.AddDefaultPolicy(b => {
        /*
         b.WithOrigins(new string[] { " http://127.0.0.1:5173/" }) 允许那些域名
        .AllowAnyMethod() 允许任意请求  比如post put get 
        .AllowAnyHeader() 接受任意的报文头
        .AllowCredentials() 也接受任意的认证方式
        .AllowAnyOrigin 允许所有域名
         */
        b.WithOrigins(new string[] { "http://localhost:5173", "http://127.0.0.1:5173" })
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});
//注入到service里面
builder.Services.AddScoped<Calculator>();
builder.Services.AddScoped<TestService>();
//增加多少个服务都OK
var asms = ReflectionHelper.GetAllReferencedAssemblies();
builder.Services.RunModuleInitializers(asms);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();//启用 cors
app.UseHttpsRedirection();

app.UseAuthorization();
//启用服务器端的响应缓存
app.UseResponseCaching();
app.MapControllers();

app.Run();
