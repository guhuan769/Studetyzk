using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
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

//读取配置
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        var jWTSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
        byte[] keyBytes = Encoding.UTF8.GetBytes(jWTSettings.SecKey);
        var secKey = new SymmetricSecurityKey(keyBytes);
        opt.TokenValidationParameters = new()
        {
            //效验过期时间ValidateLifetime设置为true ValidateIssuerSigningKey效验签名 IssuerSigningKey签名时什么
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secKey
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 312321321FDSFHDHSFDUKHFSDKFD'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
