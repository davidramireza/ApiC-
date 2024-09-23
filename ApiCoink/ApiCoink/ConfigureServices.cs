using ApiCoink.Data;
using Microsoft.EntityFrameworkCore;


public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configurar DbContext como singleton
        IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Otras configuraciones...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Aplicar migraciones automáticamente y generar las tablas
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        // Resto del pipeline de la aplicación...
    }
}