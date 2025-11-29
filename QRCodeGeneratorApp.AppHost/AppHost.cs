var builder = DistributedApplication.CreateBuilder(args);


var mongoServer = builder.AddMongoDB("mongodb-server").WithLifetime(ContainerLifetime.Persistent)
                         .WithMongoExpress().WithLifetime(ContainerLifetime.Persistent);

var mongoDb = mongoServer.AddDatabase("qrcodegeneratorapp-db");


builder.AddProject<Projects.QRCodeGeneratorApp_Api>("qrcodegeneratorapp-api")
       .WithReference(mongoDb)
       .WaitFor(mongoDb);




builder.Build().Run();
