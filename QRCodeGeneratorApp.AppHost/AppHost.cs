var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.QRCodeGeneratorApp_Api>("qrcodegeneratorapp-api");

builder.Build().Run();
