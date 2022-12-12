using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany
{
    /*
     该方法主要使用FluentAPI
     */
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //对应的表名
            builder.ToTable("T_Article");
            //配置一下其他的属性
            builder.Property(x => x.Title).HasMaxLength(100).IsUnicode().IsRequired();
            builder.Property(x => x.Message).IsUnicode().IsRequired();
            //多对一
            //HasForeignKey单独指定外键
            builder.HasMany(a => a.Comments).WithOne(x => x.Article).HasForeignKey(x => x.ArticleId).IsRequired();
        }
    }
}
