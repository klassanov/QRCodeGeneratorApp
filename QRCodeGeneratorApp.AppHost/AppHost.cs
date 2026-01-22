using QRCodeGeneratorApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);


var mongoServer = builder.AddMongoDB("mongodb-server").WithLifetime(ContainerLifetime.Persistent)
                         .WithMongoExpress();

var mongoDb = mongoServer.AddDatabase("qrcodegeneratorapp-db");

builder.AddProject<Projects.QRCodeGeneratorApp_Api>("qrcodegeneratorapp-api")      
       .WithOtelEnvironmentVariables(builder.Configuration)
       .WithReference(mongoDb)
       .WaitFor(mongoDb);




builder.Build().Run();
