using Bogus;
using Scenius.Blogpost.LoadTesting.Model;

namespace Scenius.Blogpost.LoadTesting.Service;

public class FakeOrderFactory
{
    private readonly Faker<Order> _orderFaker;

    public FakeOrderFactory()
    {
        _orderFaker = new Faker<Order>()
            .RuleFor(x => x.AmountDomesticCurrency, f => f.Finance.Amount())
            .RuleFor(x => x.AmountForeignCurrency,
                f => f.Finance.Amount()) // Conversion is nonexistent incorrect. For demonstration purposes only
            .RuleFor(x => x.ExchangeRate, f => f.Random.Decimal(0.5m, 2))
            .RuleFor(x => x.InvoiceDate, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(x => x.TransactionDate, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(x => x.CurrencyCode, f => f.Finance.Currency().Code);
    }

    public IList<Order> GetOrders(int count, List<long> productIds)
    {
        var result =  _orderFaker.Generate(count);
        var orderLineFaker = new Faker<OrderLine>()
            .RuleFor(x => x.Price, f => f.Finance.Amount())
            .RuleFor(x => x.Quantity, f => f.Random.Int(0, 10))
            .RuleFor(x => x.ProductId, f => f.Random.CollectionItem(productIds));

        foreach (var order in result)
        {
            order.OrderLines = orderLineFaker.Generate(323);
        }

        return result;
    }
}