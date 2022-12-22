using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace ClassLibrary1
{
    internal class Modellnitcs : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<Class1>();
        }
    }
}
