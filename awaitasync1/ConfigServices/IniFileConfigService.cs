using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigServices
{
    public class IniFileConfigService : IConfigService
    {
        //指定文件名
        public string FilePath { get; set; }
        public string GetValue(string name)
        {
            var file = File.ReadAllLines(FilePath).Select(s => s.Split('=')).Select(s => new { Name = s[0], Value = s[1] })
                 .SingleOrDefault(s => s.Name.Equals(name));
            if (file != null)
            {
                return file.Value;
            }
            else
            {
                return null;
            }
        }
    }
}
