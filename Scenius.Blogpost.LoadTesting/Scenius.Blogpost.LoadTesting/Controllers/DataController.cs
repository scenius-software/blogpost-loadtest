using Microsoft.AspNetCore.Mvc;
using Scenius.Blogpost.LoadTesting.Data;

namespace Scenius.Blogpost.LoadTesting.Controllers;

/// <summary>
/// For the sake of simplicity there is example logic in this controller.
/// </summary>
[Route("[controller]")]
public class DataController : Controller
{
	private readonly LoadTestBlogContext _db;
	private readonly HttpClient _httpClient;
	private static readonly SemaphoreSlim _concurrencyLatch = new(1);

	public DataController(LoadTestBlogContext db, HttpClient httpClient)
	{
		_db = db;
		_httpClient = httpClient;
	}

	[HttpGet]
	public async Task<IActionResult> GetOrders()
	{
		// Simulate slow database performance
		var orders = _db.OrderLines.Where(x => x.Product.Name.Length > 3).Average(x => x.Quantity * x.Product.WidthCentimeters);

		try
		{
			// Simulate concurrency issues
			await _concurrencyLatch.WaitAsync();
			// Simulate slow integration partner
			await _httpClient.GetAsync("https://deelay.me/5000/https://scenius.nl");
		}
		finally
		{
			_concurrencyLatch.Release();
		}
		return Ok();
	}
}