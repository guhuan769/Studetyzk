using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    //EfCore有一个约定大于配置的一个特点 
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //此地方解释明白  BOOK实体对应 T_Books这张表
            builder.ToTable("T_Books");
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AuthorName).HasMaxLength(20).IsRequired();//IsRequired 可以为空
            //Ignore 该属性忽视字段往数据库新增
            builder.Ignore(b => b.Age2);
            builder.Property(b => b.Name2).HasColumnName("NameTwo").HasColumnType("varchar(8)").HasMaxLength(50);
            builder.HasIndex(b => b.Title).IsUnique();//唯一索引
            //复合索引
            builder.HasIndex(b => new { b.Name2, b.AuthorName });//普通索引复合索引 

        }
    }
}
