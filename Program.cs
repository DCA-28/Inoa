using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockDecisor.Worker;
using StockDecisor.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<StockWorker>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<BrapiService>();

var host = builder.Build();
host.Run();