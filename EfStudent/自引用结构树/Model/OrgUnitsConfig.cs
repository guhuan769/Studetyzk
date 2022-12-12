using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自引用结构树.Model
{
    internal class OrgUnitsConfig : IEntityTypeConfiguration<OrgUnits>
    {
        public void Configure(EntityTypeBuilder<OrgUnits> builder)
        {
            builder.ToTable("T_OrgUnits");
            builder.Property(o => o.Name).IsUnicode().IsRequired(true).HasMaxLength(50);
            //此处最主要了HasOne指向自己 WithMany多个 //.OnDelete(DeleteBehavior.Restrict)
            builder.HasOne<OrgUnits>(o => o.Parent).WithMany(o => o.Children);//根节点没有Parent因此这个关系不能修饰成不可为空
        }
    }
}
