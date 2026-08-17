using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO; // Обязательно для Path и Directory

namespace snoperase.Infastrucure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Текущая папка: .../snop-rase/snope/snoperase.Infastrucure
        // 2. Поднимаемся на уровень вверх ("..") и заходим в "snope-rase"
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "snope-rase");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // Теперь это корректный путь к ПАПКЕ
            .AddJsonFile("appsettings.json") // Ищет файл ВНУТРИ этой папки
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}