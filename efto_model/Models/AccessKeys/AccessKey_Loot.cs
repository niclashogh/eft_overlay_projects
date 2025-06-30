using efto_model.Models.Enums;

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

    public static partial class AccessKey_SQLContext
    {
        public static string Loot_Table_Name { get; } = "AccessKey_Loot";

        public static List<SQLProperty> Loot_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("AccessKeyId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(AccessKey), nameof(AccessKey.Id))),
            new("Type", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(AccessKey_Loot_Type), nameof(AccessKey_Loot_Type.Type))),
            new("Quantity", SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull)
        };
    }
}
