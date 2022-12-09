using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany
{
    public class Comment
    {
        public long Id { get; set; }
        //该属性在EF中的叫法为导航属性
        public Article Article { get; set; }
        public string Message { get; set; }
        public long ArticleId { get; set; }
    }
}
