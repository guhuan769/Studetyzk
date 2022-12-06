using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//可覆盖的配置服务器
namespace ConfigServices
{
    public class LayeredConfigRead : IConfigReader
    {
        private readonly IEnumerable<IConfigService> configService;
        public LayeredConfigRead(IEnumerable<IConfigService> configService)
        {
            this.configService = configService;
        }
        public string GetValue(string name)
        {
            string value = null;
            foreach (var item in configService)
            {
                string newValue = item.GetValue(name);
                if (!string.IsNullOrEmpty(newValue))
                    value = newValue;//最后一个不为NULL的值，就是最终值
            }
            return value;
        }
    }
}
