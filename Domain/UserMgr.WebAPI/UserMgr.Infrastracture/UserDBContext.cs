using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserMgr.Domain.Entities;

namespace UserMgr.Infrastracture
{
    public class UserDBContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistorys { get; set; }

        public UserDBContext(DbContextOptions<UserDBContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从当前程序集开始加载
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
