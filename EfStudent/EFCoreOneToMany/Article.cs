using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Message { get; set; }
        //该属性在EF中的叫法为导航属性
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
