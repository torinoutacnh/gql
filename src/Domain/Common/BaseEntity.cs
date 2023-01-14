using System;
using System.ComponentModel.DataAnnotations.Schema;
using GraphQL;

namespace gql.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    protected BaseEntity() 
    {
        Id= Guid.NewGuid();
    }

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
