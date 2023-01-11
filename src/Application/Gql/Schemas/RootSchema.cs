using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gql.Application.Gql.Queries;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace gql.Application.Gql.Schemas;

public class RootSchema : Schema
{
    public RootSchema(IServiceProvider services) : base(services)
    {
        Query = services.GetRequiredService<RootQuery>();
    }
}
