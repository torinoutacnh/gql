using MediatR;

namespace gql.Domain.Common;

public abstract class BaseEvent : INotification
{
    public string Name => this.GetType().Name;
}
