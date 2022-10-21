using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Repositories;
using FurAniJoGa.WebHost.RabbitMqListener.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
                {
                    services.AddMassTransit(configurator =>
                    {
                        var host = context.Configuration["RABBITMQ_HOST"];
                        if (host is null)
                        {
                            throw new ArgumentNullException(nameof(host), "Host for RabbitMq is not provided");
                        }

                        var exchange = context.Configuration["RABBITMQ_EXCHANGE"];
                        if (exchange == null)
                        {
                            throw new ArgumentNullException(nameof(exchange), "RabbitMq Exchange name is not provided");
                        }

                        configurator.AddConsumer<MessagePublishedEventConsumer>();
                        configurator.UsingRabbitMq((registrationContext, factory) =>
                        {
                            factory.Host(host, "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                            factory.ReceiveEndpoint(e =>
                            {
                                e.Bind(exchange);
                                e.ConfigureConsumer<MessagePublishedEventConsumer>(registrationContext);
                            });
                            factory.ConfigureEndpoints(registrationContext);
                        });
                    });
                    services.AddDbContext<MessagesDbContext>(x =>
                    {
                        var database = context.Configuration["DB_DATABASE"] ?? throw new ArgumentNullException(null, "Database name is not provided");
                        var host = context.Configuration["DB_HOST"] ?? throw new ArgumentNullException(null, "Database host is not provided");;
                        var username = context.Configuration["DB_USERNAME"] ?? throw new ArgumentNullException(null, "Database username is not provided");;
                        var password = context.Configuration["DB_PASSWORD"]  ?? throw new ArgumentNullException(null, "Database password is not provided");;
                        var portString = context.Configuration["DB_PORT"] ?? throw new ArgumentNullException(null, "Database port is not provided");;
                        if (!int.TryParse(portString, out var port))
                        {
                            throw new ArgumentException($"Database port must be integer. Given: {portString}");
                        }

                        x.UseNpgsql($"User Id={username};Password={password};Host={host};Database={database};Port={port}");
                    });
                    services.AddScoped<IMessageRepository, MessageRepository>();
                    services.AddScoped<IMessageFactory, SampleMessageFactory>();
                })
               .Build();

await host.RunAsync();