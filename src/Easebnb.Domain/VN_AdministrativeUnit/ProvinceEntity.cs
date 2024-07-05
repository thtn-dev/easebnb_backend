using Easebnb.Shared.Entities;

namespace Easebnb.Domain.VN_AdministrativeUnit
{
    public class ProvinceEntity : EntityBase<string>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }
        public int? AdministrativeUnitId { get; set; }
        public int? AdministrativeRegionId { get; set; }
        public AdministrativeUnitEntity AdministrativeUnit { get; set; }
        public AdministrativeRegionEntity AdministrativeRegion { get; set; }
        public virtual ICollection<DistrictEntity> Districts { get; set; }
    }
}
