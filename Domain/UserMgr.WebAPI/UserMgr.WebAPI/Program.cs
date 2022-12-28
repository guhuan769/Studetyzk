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

//��ʽ��Ŀ�˴�ʹ��redis�˴�Ϊ�����ξ�ʹ���ڴ�
builder.Services.AddDistributedMemoryCache();
//Assembly.GetExecutingAssembly ��ȡ��ǰ�ó���
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//ע��
//ע��UserDBContext 
builder.Services.AddDbContext<UserDBContext>(o => {
    o.UseSqlServer("Server=.;Database=ddd1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
});

builder.Services.Configure<MvcOptions>(o => {
    o.Filters.Add<UnitOfWorkFilter>();
});

//ע��UserDomain
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository,UserRepository> ();
//Ӧ�ò���з����ƴװ
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
