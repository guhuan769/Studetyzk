
using CommonInitializer;
using FileService.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace MediaEncoder.WebAPI;

//用IDesignTimeDbContextFactory坑最少，最省事
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FSDbContext>
{
    public FSDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<FSDbContext>();
        return new FSDbContext(optionsBuilder.Options, null);
    }
}
