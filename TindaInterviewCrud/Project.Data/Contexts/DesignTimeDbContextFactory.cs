using GMF.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Data.Contexts;
using System.IO;
namespace GMF.Data.DAL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbcontext>
{
    public AppDbcontext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
        .Build();


        var optionsBuilder = new DbContextOptionsBuilder<AppDbcontext>();
        var connectionString = configuration.GetConnectionString("Default");
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbcontext(optionsBuilder.Options);
    }
}