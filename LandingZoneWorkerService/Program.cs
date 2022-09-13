using LandingZoneWorkerService;
using Serilog;
using Serilog.Enrichers;
using System.IO;

IHost host = Host.CreateDefaultBuilder(args)  
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetry();
        Log.Logger = new LoggerConfiguration()
         .Enrich.WithProperty("Version", "1.0.0")
        //.Enrich.With(new ThreadIdEnricher())
        .MinimumLevel.Debug()
        .WriteTo.Console()
        //.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
        .WriteTo.File("D:/logs/myapp.txt")
        .CreateLogger();
        services.AddHostedService<Worker>();
        //services.AddHostedService<PingerService>();
    })
    
    .Build();

await host.RunAsync();
