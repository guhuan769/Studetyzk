using ClassLibrary1;
using WebAPI;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

//��仰��controllerע�뵽DI��
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region ��var app = builder.Build();֮ǰ����CORS
builder.Services.AddCors(opt => {
    opt.AddDefaultPolicy(b => {
        /*
         b.WithOrigins(new string[] { " http://127.0.0.1:5173/" }) ������Щ����
        .AllowAnyMethod() ������������  ����post put get 
        .AllowAnyHeader() ��������ı���ͷ
        .AllowCredentials() Ҳ�����������֤��ʽ
        .AllowAnyOrigin ������������
         */
        b.WithOrigins(new string[] { "http://localhost:5173", "http://127.0.0.1:5173" })
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});
//ע�뵽service����
builder.Services.AddScoped<Calculator>();
builder.Services.AddScoped<TestService>();
//���Ӷ��ٸ�����OK
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

app.UseCors();//���� cors
app.UseHttpsRedirection();

app.UseAuthorization();
//���÷������˵���Ӧ����
app.UseResponseCaching();
app.MapControllers();

app.Run();
