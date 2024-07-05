using Easebnb.Shared.Entities;

namespace Easebnb.Domain.VN_AdministrativeUnit
{
    public class AdministrativeRegionEntity : EntityBase<int>
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string CodeName { get; set; }
        public string CodeNameEn { get; set; }
        public virtual ICollection<ProvinceEntity> Provinces { get; set; }
    }
}
