using Elastic.Apm.NetCoreAll;
using Scenius.Blogpost.LoadTesting.Data;
using Microsoft.EntityFrameworkCore;
using Scenius.Blogpost.LoadTesting.Service;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5000");

// Setup APM
builder.Host.UseAllElasticApm();

// Setup DI
builder.Services.AddHostedService<DataSeederBackgroundService>();
builder.Services.AddHttpClient<DatabaseFillerService>();

// Setup DB
builder.Services.AddDbContext<LoadTestBlogContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("BloggingContext")));

builder.Services.AddControllers();


// Setup web pipeline. Dense stuff this .NET6 template
var app = builder.Build();

app.UsePathBase("/api");
app.MapControllers();
app.Run();