using System.Reflection;
using System.Threading.Channels;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;




namespace DataAccessLayer;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;   

    public ApplicationContext() : base(GetOptions())
    {
        Database.EnsureCreated();
    }

    private static DbContextOptions<ApplicationContext> GetOptions()
    {
        /*var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        var configPath = Path.Combine(assemblyDirectory, "appsettings.json");*/

        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        /*var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            */


        // "Server=127.0.0.1;Port=5432;User Id=postgres;Password=pass;Include Error Detail=True"; 
        var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionBuilder.UseNpgsql(config.GetConnectionString("Postgres"));

        return optionBuilder.Options;
    }
}   