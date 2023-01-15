using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Domain.Entities;

namespace gql.Application.gql.Types;

public class TodoItemType : ObjectType<TodoItem>
{
    protected override void Configure(
        IObjectTypeDescriptor<TodoItem> descriptor)
    {
        descriptor.Ignore(x=>x.DomainEvents);
        descriptor.Field(i=>i.List)
            .Type<TodoListType>();
    }
}
