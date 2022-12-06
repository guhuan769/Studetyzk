using ConfigServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayeredConfigReadExtensions
    {
        public static void AddLayeredConfigRead(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IConfigReader, LayeredConfigRead>();
        }
    }
}
