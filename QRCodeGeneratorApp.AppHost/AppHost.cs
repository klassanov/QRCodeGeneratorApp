var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.QRCodeGeneratorApp_Api>("qrcodegeneratorapp-api");
    //.WithUrlForEndpoint("https", url =>
    //{
    //    url.DisplayText = "Healtcheck";
    //    url.Url = "/healthchecks-ui";
    //});


builder.Build().Run();
