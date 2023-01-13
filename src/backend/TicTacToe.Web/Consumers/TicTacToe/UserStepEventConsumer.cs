using MassTransit;
using TicTacToe.Web.Consumers.TicTacToe.Events;

namespace TicTacToe.Web.Consumers.TicTacToe;

public class UserStepEventConsumer: IConsumer<UserStepEvent>
{
    public Task Consume(ConsumeContext<UserStepEvent> context)
    {
        throw new NotImplementedException();
    }
}