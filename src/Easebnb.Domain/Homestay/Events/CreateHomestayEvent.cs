using Easebnb.Shared;
using MediatR;

namespace Easebnb.Domain.Homestay.Events;

public record CreateHomestayEvent : DomainEventBase
{
    public string HomestayId { get; }
    public CreateHomestayEvent(string homestayId)
    {
        HomestayId = homestayId;
    }
}

