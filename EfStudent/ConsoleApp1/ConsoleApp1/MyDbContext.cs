using ConsoleApp1;
using EFCORE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    class MyDbContext : DbContext
    {
        //public DbSet<Book> Books { get; set; } Books为表名
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Dog> Dog { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Bird> BirdHAHAHA { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=localhost; Database=Demo; Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Server=.;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer("Server=.;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this.GetType().Assembly 得到当前项目的程序集 从当前程序集加载
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
