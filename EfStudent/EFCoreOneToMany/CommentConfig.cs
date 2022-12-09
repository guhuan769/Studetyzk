using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.Property(a => a.Message).IsRequired();

            //一个对多  IsRequired(不允许为空就.该方法) 创建主外键  ArticleID为主键 Comment表的AritcleID为外键
            //设置 HasForeignKey 外键属性 对应实体
            builder.HasOne<Article>(x => x.Article).WithMany(a => a.Comments).HasForeignKey(x => x.ArticleId).IsRequired();

        }
    }
}
