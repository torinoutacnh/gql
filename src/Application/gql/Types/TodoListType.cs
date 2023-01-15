using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Domain.Entities;

namespace gql.Application.gql.Types;

public class TodoListType : ObjectType<TodoList>
{
    protected override void Configure(
        IObjectTypeDescriptor<TodoList> descriptor)
    {
        descriptor.Ignore(x=>x.DomainEvents);
    }
}
