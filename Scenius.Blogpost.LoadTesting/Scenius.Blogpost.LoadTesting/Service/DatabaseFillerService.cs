using Scenius.Blogpost.LoadTesting.Data;

namespace Scenius.Blogpost.LoadTesting.Service;

public class DatabaseFillerService
{
    private readonly ILogger<DatabaseFillerService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private const int OrderCount = 1000000;
    private const int BatchSize = 10000;

    public DatabaseFillerService(ILogger<DatabaseFillerService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void Fill()
    {
        var productIds = new List<long>();
        var productFactory = new FakeProductFactory();

        for (var i = 0; i < OrderCount; i += BatchSize)
        {
            using var scope = _serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<LoadTestBlogContext>();
            var products = productFactory.GetOrders(BatchSize);
            db.Products.AddRange(products);
            db.SaveChanges();
            productIds.AddRange(products.Select(x => x.Id));
        }

        var orderFactory = new FakeOrderFactory();
        for (var i = 0; i < OrderCount; i += BatchSize)
        {
            using var scope = _serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<LoadTestBlogContext>();
            var orders = orderFactory.GetOrders(BatchSize, productIds);
            db.AddRange(orders);
            db.SaveChanges();
        }
    }
}