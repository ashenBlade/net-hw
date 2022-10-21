using FurAniJoGa.WebHost.FileAPI.MetadataUpdater;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                  {
                      throw new NotImplementedException();
                  })
                 .Build();

await host.RunAsync();