using Easebnb.Shared;

namespace Easebnb.Domain.Homestay.Events;

public class CreateHomestayEvent : DomainEventBase
{
    public string HomestayId { get; }
    public CreateHomestayEvent(string homestayId)
    {
        HomestayId = homestayId;
    }
}