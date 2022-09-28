using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using MassTransit;
using MessagesListener;
using MessagesListener.Consumers;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services =>
                  {
                      services.AddDbContext<MessagesDbContext>(x =>
                      {
                          x.UseNpgsql("User Id=postgres;Password=postgres;Host=database;Port=5432;Database=postgres");
                      });
                      services.AddScoped<IMessageFactory, DbMessageFactory>();
                      
                      services.AddMassTransit(configurator =>
                      {
                          const string defaultQueue = "messages-listener";
                          const string defaultExchange = "amq.fanout";
                          configurator.AddConsumer<MessagePublishedConsumer>();
                          configurator.UsingRabbitMq((context, factory) =>
                          {
                              factory.Host("rabbit-mq", "/", h =>
                              {
                                  h.Username("guest");
                                  h.Password("guest");
                              });
                              
                              factory.ReceiveEndpoint(defaultQueue, e =>
                              {
                                  e.UseJsonDeserializer();
                                  e.Bind(defaultExchange);
                                  e.Durable = false;
                                  e.Exclusive = true;
                                  e.ConfigureConsumer<MessagePublishedConsumer>(context);
                              });
                              factory.ConfigureEndpoints(context);
                          });
                      });
                      services.AddHostedService<RabbitMqListener>();
                  })
                 .Build();
await host.RunAsync();