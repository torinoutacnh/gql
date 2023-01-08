using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gql.Domain.Common;
internal interface ISoftDeleted
{
    bool IsDeleted { get; }
}
