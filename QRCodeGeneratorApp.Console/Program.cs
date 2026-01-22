using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRGeneratorApp.Core;
using QRGeneratorApp.Core.GridCreation;
using QRGeneratorApp.Core.QRMapCreation;


var builder = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    var gridConfig = new GridConfig();
    services.AddSingleton(gridConfig);
    services.RegisterCoreServices();
});

var app = builder.Build();

var qrMapCreator = app.Services.GetRequiredService<IQRMapCreator>();
var qrMap = qrMapCreator.GenerateQRMap("Саааааамо Левски!");
var gridCreator = app.Services.GetRequiredService<IGridCreator>();
var filepath = gridCreator.CreateGrid(qrMap);

Console.WriteLine($"File saved to {filepath}");
