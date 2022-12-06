using ConfigServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IniFileConfigServiceExtensions
    {
        public static void AddIniFileConfig(this IServiceCollection  serviceCollection,string filePath)
        {
            serviceCollection.AddScoped(typeof(IConfigService),s=> new IniFileConfigService() { FilePath = filePath });
        }
    }
}
