using Abp.Domain.Entities;

namespace Models.Models.DBEntities
{
    public class VehicleType : Entity, IEntity
    {
        public string TypeName { get; set; }
    }
}
