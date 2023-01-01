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

//��ȡ����
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        var jWTSettings = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
        byte[] keyBytes = Encoding.UTF8.GetBytes(jWTSettings.SecKey);
        var secKey = new SymmetricSecurityKey(keyBytes);
        opt.TokenValidationParameters = new()
        {
            //Ч�����ʱ��ValidateLifetime����Ϊtrue ValidateIssuerSigningKeyЧ��ǩ�� IssuerSigningKeyǩ��ʱʲô
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
