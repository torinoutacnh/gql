using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;

namespace gql.Application.Gql.Commons;

public class AuditableEntityResolver : IAuditableEntityResolver
{
    private readonly Dictionary<Type, Type> _dict;

    public AuditableEntityResolver(Dictionary<Type, Type> dict)
    {
        _dict = dict;
    }

    public Type ResolveObject(Type graphType) => _dict[graphType];
}

public interface IAuditableEntityResolver
{
    public Type ResolveObject(Type graphType);
}

