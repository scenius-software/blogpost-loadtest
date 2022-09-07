using Bogus;
using Scenius.Blogpost.LoadTesting.Model;

namespace Scenius.Blogpost.LoadTesting.Service;

public class FakeProductFactory
{
    private readonly Faker<Product> _productFaker;

    public FakeProductFactory()
    {
        _productFaker = new Faker<Product>()
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Barcode, f => f.Commerce.Ean8())
            .RuleFor(x => x.EAN, f => f.Commerce.Ean13())
            .RuleFor(x => x.UPC, f => f.Random.String2(8))
            .RuleFor(x => x.WeightKilograms, f => f.Random.Decimal(0.1m, 150))
            .RuleFor(x => x.HeightCentimeters, f => f.Random.Decimal(0.1m, 150))
            .RuleFor(x => x.WidthCentimeters, f => f.Random.Decimal(0.1m, 150))
            .RuleFor(x => x.LengthCentimeters, f => f.Random.Decimal(0.1m, 150))
            .RuleFor(x => x.TaxCode, "NL_RATE_HIGH")
            .RuleFor(x => x.ProductCategory, f => string.Join(',', f.Commerce.Categories(6)));
    }

    public IList<Product> GetOrders(int count)
    {
        return _productFaker.Generate(count);
    }
}