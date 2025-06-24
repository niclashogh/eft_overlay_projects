namespace efto_model.Models.AccessKeys
{
    public class AccessKey_Loot
    {
        public int Id { get; set; }
        public int AccessKeyId { get; set; }
        public string Type { get; set; }
        public byte Quantity { get; set; }

        public AccessKey_Loot(int accessKeyId, string type, byte quantity)
        {
            this.AccessKeyId = accessKeyId;
            this.Type = type;
            Quantity = quantity;
        }

        public AccessKey_Loot() { }
    }
}
