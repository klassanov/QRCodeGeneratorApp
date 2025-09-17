// See https://aka.ms/new-console-template for more information
using QRCodeGeneratorApp.Console;

Console.WriteLine("Hello, World!");

var qrMap= QRManager.GenerateQRMap("Бониии! Как сме?");
var filepath = GridCreator.CreateGrid(qrMap);
Console.WriteLine($"File saved to {filepath}");
