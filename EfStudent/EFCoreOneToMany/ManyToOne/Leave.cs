using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany.ManyToOne
{
    public class Leave
    {
        public long Id { get; set; }
        public string Title { get; set; }
        //申请人 NET6中单项导航属性对应实体切记一定要加？不会Update-Database报错
        //public User? RequesterId { get;set; }
        public User RequesterId { get;set; }
        //审批人
        //public User? ApproverId { get;set; }
        public User ApproverId { get;set; }

        public string Remark { get; set; }
    }
}
