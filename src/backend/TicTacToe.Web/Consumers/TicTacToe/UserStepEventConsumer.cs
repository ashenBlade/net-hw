using MassTransit;
using Microsoft.AspNetCore.Components;
using TicTacToe.Web.Consumers.TicTacToe.Events;

namespace TicTacToe.Web.Consumers.TicTacToe;

public class UserStepEventConsumer: IConsumer<UserStepEvent>
{
    private readonly ILogger<UserStepEventConsumer> _logger;

    public UserStepEventConsumer(ILogger<UserStepEventConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<UserStepEvent> context)
    {
        throw new NotImplementedException();
    }
}