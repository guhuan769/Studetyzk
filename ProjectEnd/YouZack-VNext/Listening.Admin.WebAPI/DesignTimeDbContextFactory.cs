
using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;

namespace MediaEncoder.WebAPI;

//用IDesignTimeDbContextFactory坑最少，最省事
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ListeningDbContext>
{
    public ListeningDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<ListeningDbContext>();
        return new ListeningDbContext(optionsBuilder.Options, null);
    }
}
