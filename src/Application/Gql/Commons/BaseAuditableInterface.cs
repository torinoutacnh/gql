using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using gql.Domain.Common;
using GraphQL.Types;

namespace gql.Application.Gql.Commons;

public class BaseAuditableInterface: AutoRegisteringInterfaceGraphType<BaseAuditableEntity>
{
    public BaseAuditableInterface(IAuditableEntityResolver auditableEntityResolver): base(x => x.DomainEvents)
    {
        Name = nameof(BaseAuditableInterface);

        ResolveType = obj =>
        {
            var name = auditableEntityResolver.ResolveObject(obj.GetType()).Name;
            return new GraphQLTypeReference(name);

            throw new ArgumentOutOfRangeException($"Could not resolve graph type for {obj.GetType().Name}");
        };
    }
}
