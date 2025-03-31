using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class AccessKey_Loot
    {
        public int Id { get; set; }
        public int AccessKeyId { get; set; }
        public AccessKey_Loot_Types Type { get; set; }
        public byte Quantity { get; set; }

        public AccessKey_Loot(int accessKeyId, AccessKey_Loot_Types type, byte quantity)
        {
            this.AccessKeyId = accessKeyId;
            this.Type = type;
            this.Quantity = quantity;
        }

        public AccessKey_Loot() { }
    }
}
