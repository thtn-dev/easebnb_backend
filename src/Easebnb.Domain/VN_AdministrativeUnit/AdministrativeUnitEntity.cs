using Easebnb.Shared.Entities;

namespace Easebnb.Domain.VN_AdministrativeUnit
{
    public class AdministrativeUnitEntity : EntityBase<int>
    {
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string ShortName { get; set; }
        public string ShortNameEn { get; set; }
        public string CodeName { get; set; }
        public string CodeNameEn { get; set; }
        public virtual ICollection<ProvinceEntity> Provinces { get; set; }
        public virtual ICollection<DistrictEntity> Districts { get; set; }
        public virtual ICollection<WardEntity> Wards { get; set; }
    }
}
