var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();

app.Run();
