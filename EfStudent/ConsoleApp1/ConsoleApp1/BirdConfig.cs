using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCORE
{
    //FlunAPI
    internal class BirdConfig : IEntityTypeConfiguration<Bird>
    {
        public void Configure(EntityTypeBuilder<Bird> builder)
        {
            builder.HasKey(x => x.Number);
            //设置字段默认值
            builder.Property(x => x.Name).HasDefaultValue("Hello");
        }
    }
}
