using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserMgr.Domain;
using UserMgr.Infrastracture;
using UserMgr.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//正式项目此处使用redis此处为了掩饰就使用内存
builder.Services.AddDistributedMemoryCache();
//Assembly.GetExecutingAssembly 获取当前得程序集
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//注册
//注册UserDBContext 
builder.Services.AddDbContext<UserDBContext>(o => {
    o.UseSqlServer("Server=.;Database=ddd1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
});

builder.Services.Configure<MvcOptions>(o => {
    o.Filters.Add<UnitOfWorkFilter>();
});

//注册UserDomain
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository,UserRepository> ();
//应用层进行服务得拼装
builder.Services.AddScoped<ISmsCodeSender, MockSmsCodeSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
