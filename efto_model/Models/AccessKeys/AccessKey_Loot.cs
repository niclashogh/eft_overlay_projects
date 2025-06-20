namespace efto_model.Models.AccessKeys
{
    public class AccessKey_Loot
    {
        public int Id { get; set; }
        public int AccessKeyId { get; set; }
        public int TypeId { get; set; }
        public byte Quantity { get; set; }

        public AccessKey_Loot(int accessKeyId, int typeId, byte quantity)
        {
            this.AccessKeyId = accessKeyId;
            this.TypeId = typeId;
            Quantity = quantity;
        }

        public AccessKey_Loot() { }
    }
}
