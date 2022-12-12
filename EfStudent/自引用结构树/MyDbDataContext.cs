using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 自引用结构树.Model;

namespace EFCORETEST2
{

    /**/
    public class MyDbDataContext : DbContext
    {
        //可以将执行的linq 转换成SQL语句打印到控制台
        //private static ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddConsole());
        public DbSet<OrgUnits> OrgUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=Demo10;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            ////2 简单日志 优点 不用引入第三方框架
            //optionsBuilder.LogTo(msg =>
            //{
            //    //只输出SQL语句
            //    if (!msg.Contains("CommandExecuting")) return;
            //    Console.WriteLine(msg);
            //});


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this.GetType().Assembly 得到当前项目的程序集 从当前程序集加载
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
