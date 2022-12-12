using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自引用结构树.Model
{
    public class OrgUnits
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public OrgUnits? Parent { get; set; }
        public List<OrgUnits?> Children { get; set; } = new List<OrgUnits?>();
    }
}
