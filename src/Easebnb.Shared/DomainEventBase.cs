using MediatR;

namespace Easebnb.Shared;
public abstract record DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}