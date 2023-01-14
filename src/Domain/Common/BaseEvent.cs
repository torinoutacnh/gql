using GraphQL.Types;
using MediatR;

namespace gql.Domain.Common;

public abstract class BaseEvent : INotification
{
}

public class BaseEventType : ObjectGraphType<BaseEvent> 
{
    public BaseEventType()
    {
        Field<StringGraphType, string>("EventName").Resolve(x => "Events");
    }
}
