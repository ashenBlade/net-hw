using System.Reflection;
using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater;
using FurAniJoGa.Worker.MongoUpdater.Consumers;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;
using FurAniJoGa.Worker.MongoUpdater.FileMoveService;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;
using MediatR;

using var host = Host.CreateDefaultBuilder(args)
                     .ConfigureServices((context, services) =>
                      {
                          services.AddSingleton(context.Configuration.GetRedisSettings());
                          services.AddScoped<IUploadRequestRepository, RedisUploadRequestRepository>();
                          services.AddScoped<IFileUploaderCounterService, RedisFileUploaderCounterService>();

                          services.AddSingleton(context.Configuration.GetMongoSettings());
                          services.AddScoped<IFileMetadataRepository, MongoFileMetadataRepository>();
                      
                          services.AddMassTransit(configurator =>
                          {
                              configurator.AddConsumer<FileUploadedEventConsumer>();
                              configurator.AddConsumer<MetadataUploadedEventConsumer>();
                              configurator.AddConsumer<FileAndMetadataUploadedEventConsumer>();
                              configurator.UsingRabbitMq((registrationContext, factory) =>
                              {
                                  var host = context.Configuration.GetValue<string>("RABBITMQ_HOST");
                                  var exchange = context.Configuration.GetValue<string>("RABBITMQ_EXCHANGE");
                                  factory.Host(host, "/", h =>
                                  {
                                      h.Username("guest");
                                      h.Password("guest");
                                  });
                                  factory.ReceiveEndpoint(e =>
                                  {
                                      e.Bind(exchange);
                                      e.ConfigureConsumer<FileUploadedEventConsumer>(registrationContext);
                                      e.ConfigureConsumer<MetadataUploadedEventConsumer>(registrationContext);
                                      e.ConfigureConsumer<FileAndMetadataUploadedEventConsumer>(registrationContext);
                                  });
                                  factory.ConfigureEndpoints(registrationContext);
                              });
                          });

                          services.AddSingleton(context.Configuration.GetS3FileMoveServiceOptions());
                          services.AddScoped<IFileMoveService, S3FileMoveService>();
                          services.AddMediatR(Assembly.GetExecutingAssembly());
                      })
                     .Build();

await host.RunAsync();