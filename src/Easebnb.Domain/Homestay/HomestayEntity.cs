
using Easebnb.Domain.Homestay.Events;
using Easebnb.Shared.Entities;

namespace Easebnb.Domain.Homestay;
public class HomestayEntity : EntityAggregateBase<string>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public HomestayEntity()
    {
        Id = Guid.NewGuid().ToString();
        RegisterDomainEvent(new CreateHomestayEvent(Id));
    }
}