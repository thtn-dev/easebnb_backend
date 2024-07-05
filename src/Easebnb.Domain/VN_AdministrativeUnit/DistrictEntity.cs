using Easebnb.Shared.Entities;

namespace Easebnb.Domain.VN_AdministrativeUnit
{
    public class DistrictEntity : EntityBase<string>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }
        public string ProvinceCode { get; set; }
        public int? AdministrativeUnitId { get; set; }
        public ProvinceEntity Province { get; set; }
        public AdministrativeUnitEntity AdministrativeUnit { get; set; }
        public virtual ICollection<WardEntity> Wards { get; set; }
    }
}
