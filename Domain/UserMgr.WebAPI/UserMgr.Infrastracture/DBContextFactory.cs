using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Infrastracture
{
    internal class DBContextFactory : IDesignTimeDbContextFactory<UserDBContext>
    {
        /// <summary>
        /// 该类是用来  Add-Migration 
        /// 如果要使用Add-Migration进行数据迁移 必须安装该包 Install-Package Microsoft.EntityFrameworkCore.Tools
        /// 在更新数据库 Update-DataBase 数据库才会有目标库
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public UserDBContext CreateDbContext(string[] args)
        {
            //DbContextOptionsBuilder<UserDBContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<UserDBContext>();
            var builder = new DbContextOptionsBuilder<UserDBContext>();
            builder.UseSqlServer("Server=.;Database=ddd1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            return new UserDBContext(builder.Options);
        }
    }
}
