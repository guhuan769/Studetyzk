﻿using EFCoreOneToMany.ManyToOne;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreOneToMany
{
    public class MyDbContext: DbContext
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=OneToMany;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
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
