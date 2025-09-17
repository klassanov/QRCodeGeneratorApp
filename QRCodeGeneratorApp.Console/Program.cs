// See https://aka.ms/new-console-template for more information
using QRGeneratorApp.Core.GridCreation;
using QRGeneratorApp.Core.QRMapCreation;

Console.WriteLine("Hello, World!");

var qrMap= QRMapCreator.GenerateQRMap("Бониии! Как сме днес?");
var filepath = GridCreator.CreateGrid(qrMap);
Console.WriteLine($"File saved to {filepath}");
