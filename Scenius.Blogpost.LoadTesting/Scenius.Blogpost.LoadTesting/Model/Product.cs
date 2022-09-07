namespace Scenius.Blogpost.LoadTesting.Model;

public class Product
{
    public long Id { get; set; }
	
    public string? Name { get; set; }
    public decimal WeightKilograms { get; set; }
    public decimal HeightCentimeters { get; set; }
    public decimal WidthCentimeters { get; set; }
    public decimal LengthCentimeters { get; set; }
    public string? UPC { get; set; }
    public string? EAN { get; set; }
    public string? Barcode { get; set; }
    public string? TaxCode { get; set; }
    public string? ProductCategory { get; set; }
}