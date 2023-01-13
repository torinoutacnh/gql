using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using gql.Domain.Common;
using GraphQL.Types;

namespace gql.Application.Gql.Commons;

public class BaseAuditableInterface: InterfaceGraphType<BaseAuditableEntity>
{
    public BaseAuditableInterface(IAuditableEntityResolver auditableEntityResolver) 
    {
        Name = nameof(BaseAuditableEntity);

        Field(i => i.Id).Description("TodoItem Id").Name("id");
        Field(i => i.Created).Description("Date created");
        Field(i => i.CreatedBy, nullable: true).Description("Created by user with id");
        Field(i => i.LastModified, nullable: true).Description("Last modified date");
        Field(i => i.LastModifiedBy, nullable: true).Description("Modified by user with id");
        Field(i => i.IsDeleted).Description("Deleted status");

        ResolveType = obj =>
        {
            var name = auditableEntityResolver.ResolveObject(obj.GetType()).Name;
            return new GraphQLTypeReference(name);

            throw new ArgumentOutOfRangeException($"Could not resolve graph type for {obj.GetType().Name}");
        };
    }
}
