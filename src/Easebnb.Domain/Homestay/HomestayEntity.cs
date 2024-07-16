
using Easebnb.Domain.Homestay.Events;
using Easebnb.Shared.Entities;
using NetTopologySuite.Geometries;

namespace Easebnb.Domain.Homestay;
public class HomestayEntity : EntityAggregateBase<long>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ExtraData { get; set; }
    public double Longtitude { get; set; }
    public double Latitude { get; set; }
    public Geometry? Geom { get; set; }
    public string RawAddress { get; set; } = null!;
    public HomestayEntity()
    {
    }

    public static HomestayEntity Create(long id, string name, string description)
    {
        var homestay = new HomestayEntity
        {
            Id = id,
            Name = name,
            Description = description
        };
        homestay.RegisterDomainEvent(new CreateHomestayEvent(homestay.Id));
        return homestay;
    }

    public static void SetAddress(HomestayEntity homestay, string address, double longitude, double latitude)
    {
        homestay.RawAddress = address;
        homestay.Longtitude = longitude;
        homestay.Latitude = latitude;
        homestay.Geom = new Point(longitude, latitude) { SRID = 3857 };
    }
}