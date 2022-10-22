using FurAniJoGa.WebHost.FileAPI.MetadataUpdater;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((context, services) =>
                  {
                      services.AddFileUploadedEventConsumer(context.Configuration);
                  })
                 .Build();

await host.RunAsync();