namespace Scenius.Blogpost.LoadTesting.Model;

public class OrderLine
{
    public int Id { get; set; }
	
    public long OrderId { get; set; }
    public Order Order { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ShipmentId { get; set; }
}