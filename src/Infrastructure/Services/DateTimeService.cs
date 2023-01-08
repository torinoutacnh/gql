using gql.Application.Common.Interfaces;

namespace gql.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
