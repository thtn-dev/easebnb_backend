
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
    }

    public static HomestayEntity Create(string name, string description)
    {
        var homestay = new HomestayEntity
        {
            Name = name,
            Description = description
        };
        homestay.RegisterDomainEvent(new CreateHomestayEvent(homestay.Id));
        return homestay;
    }
}