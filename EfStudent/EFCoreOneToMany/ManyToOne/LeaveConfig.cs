using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany.ManyToOne
{
    public class LeaveConfig : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.ToTable("T_Leave");
            //重点是如何配置反向的导航属性 只要withmany()不设置参数即可 .IsRequired()
            //builder.HasOne<User>(x => x.Requester).WithMany();
            //builder.HasOne<User>(x => x.Approver).WithMany();//审核人可以为空 
            //因为不需要User知道Requester所以不设置反向导航属性
            //builder.HasOne<User>(b => b.RequesterId).WithMany().IsRequired();
            //builder.HasOne<User>(b => b.ApproverId).WithMany();
            //在NET6中如果不加ondelete会报错
            builder.HasOne<User>(x => x.ApproverId).WithMany().OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne<User>(b => b.ApproverId).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
