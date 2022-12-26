using ConsoleApp1;
using Intf1;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<MyBizCode1>();
serviceCollection.AddScoped<IEmailSender, MyEmailSender>();
//serviceCollection.AddScoped<IMyDataProvider, MyDataProvider>();
serviceCollection.AddScoped<IMyDataProvider, MyDataProviderMock1>();
var sp = serviceCollection.BuildServiceProvider();
var code1 = sp.GetRequiredService<MyBizCode1>();
await code1.DoItAsync();
