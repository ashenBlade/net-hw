using FurAniJoGa.WebHost.FileAPI.MetadataUpdater;
using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Consumers;
using MassTransit;
using MongoDB.Driver;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((context, services) =>
                  {
                      services.AddFileUploadedEventConsumer(context.Configuration);
                  })
                 .Build();

await host.RunAsync();