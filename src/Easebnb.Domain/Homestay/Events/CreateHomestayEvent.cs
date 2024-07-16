using Easebnb.Shared;
using MediatR;

namespace Easebnb.Domain.Homestay.Events;

public record CreateHomestayEvent : DomainEventBase
{
    public long HomestayId { get; }
    public CreateHomestayEvent(long homestayId)
    {
        HomestayId = homestayId;
    }
}

