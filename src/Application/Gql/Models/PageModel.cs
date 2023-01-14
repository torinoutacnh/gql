using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Types;

namespace gql.Application.Gql.Models;

public class PageModel
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class PageModelInputType : AutoRegisteringInputObjectGraphType<PageModel>
{
}
