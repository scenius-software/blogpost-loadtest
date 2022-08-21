using Microsoft.EntityFrameworkCore;
using Scenius.Blogpost.LoadTesting.Data;

namespace Scenius.Blogpost.LoadTesting.Service;

public class DataSeederBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;

    public DataSeederBackgroundService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<LoadTestBlogContext>();

        if ((await db.Database.GetPendingMigrationsAsync(stoppingToken)).Any())
        {
            await db.Database.MigrateAsync(stoppingToken);
        }
        if (db.Orders.Any())
        {
            return; // No action needed, DB already contains data
        }

        var databaseFillerService = scope.ServiceProvider.GetRequiredService<DatabaseFillerService>();
        databaseFillerService.Fill();
    }
}