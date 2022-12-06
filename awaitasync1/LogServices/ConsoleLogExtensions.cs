using LogServices;
using System;
using System.Collections.Generic;
using System.Text;

//扩展方法 
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConsoleLogExtensions
    {
        //扩展方法加this 
        public static void AddConsoleLog(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILogProvider,ConsoleLogProvider>();
        }
    }
}
