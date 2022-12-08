using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class Book
    {
        public long Id { get; set; }//主键
        public string Title { get; set; } //标题
        public DateTime PubTime { get; set; }//发布时间
        public double Price { get; set; } //单价
        public string AuthorName { get; set; }


        public int Age1 { get; set; }
        public int Age2 { get; set; }

        public string? Name2 { get; set; }
    }
}
