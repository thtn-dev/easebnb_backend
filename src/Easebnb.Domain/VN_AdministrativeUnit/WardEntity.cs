

using Easebnb.Shared.Entities;

namespace Easebnb.Domain.VN_AdministrativeUnit
{
    public class WardEntity : EntityBase<string>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }
        public string DistrictCode { get; set; }
        public int? AdministrativeUnitId { get; set; }
        public DistrictEntity District { get; set; }
        public AdministrativeUnitEntity AdministrativeUnit { get; set; }
    }
}
