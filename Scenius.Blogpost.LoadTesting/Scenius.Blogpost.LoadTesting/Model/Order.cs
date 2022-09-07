namespace Scenius.Blogpost.LoadTesting.Model;

public class Order
{
	public long Id { get; set; }
	
	public decimal AmountForeignCurrency { get; set; }
	public decimal AmountDomesticCurrency { get; set; }
	public decimal ExchangeRate { get; set; }
	public DateTime InvoiceDate { get; set; }
	public DateTime TransactionDate { get; set; }
	public string? Description { get; set; }
	public string? CurrencyCode { get; set; }
	public string? ShippingAddress { get; set; }
	public List<OrderLine> OrderLines { get; set; }
}