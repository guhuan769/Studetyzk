using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 表达式树EfCore3
{
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=TwoOneNice;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //打印SQL语句
            optionsBuilder.LogTo(msg =>
            {
                //只输出SQL语句
                //if (!msg.Contains("CommandExecuting")) return;
                Console.WriteLine(msg);
            });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this.GetType().Assembly 得到当前项目的程序集 从当前程序集加载
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
