//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using gql.Application.Common.Models;
//using gql.Domain.Common;
//using GraphQL.Types;

//namespace gql.Application.Gql.Types;

//public class PaginatedListType : ObjectGraphType<PaginatedList<object>>
//{
//    public PaginatedListType()
//    {
//        Field(x => x.Items);
//        Field(x => x.PageNumber);
//        Field(x => x.HasNextPage);
//        Field(x => x.HasPreviousPage);
//    }
//}
