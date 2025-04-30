using efto_model.Models.Enums;

namespace efto_model.Models.DataTransferObjects
{
    public static class Color_DTO
    {
        public static (byte R, byte G, byte B) White { get; } = new(255, 255, 255);
        public static (byte R, byte G, byte B) Blue { get; } = new(55, 84, 169);
        public static (byte R, byte G, byte B) Red { get; } = new(169, 55, 55);
        public static (byte R, byte G, byte B) Green { get; } = new(92, 120, 91);
        public static (byte R, byte G, byte B) Orange { get; } = new(180, 123, 38);
        public static (byte R, byte G, byte B) Purple { get; } = new(76, 69, 140);
        public static (byte R, byte G, byte B) Yellow { get; } = new(169, 160, 55);
        public static (byte R, byte G, byte B) Teal { get; } = new(35, 160, 141);
        public static (byte R, byte G, byte B) Magenta { get; } = new(160, 18, 132);
        public static (byte R, byte G, byte B) Brown { get; } = new(84, 67, 47);

        public static List<(byte R, byte G, byte B)> Colors { get; } = new List<(byte R, byte G, byte B)>
        {
            White, Blue, Red, Green, Orange, Purple, Yellow, Teal, Magenta, Brown
        };

        public static (byte R, byte G, byte B) GetFromEnum(Palette color)
        {
            return color switch
            {
                Palette.White => White,
                Palette.Blue => Blue,
                Palette.Red => Red,
                Palette.Green => Green,
                Palette.Orange => Orange,
                Palette.Purple => Purple,
                Palette.Yellow => Yellow,
                Palette.Teal => Teal,
                Palette.Magenta => Magenta,
                Palette.Brown => Brown,
                _ => White
            };
        }

        public static (byte R, byte G, byte B) GetFromEnum(Extraction_Types type)
        {
            return type switch
            {
                Extraction_Types.PMC => Green,
                Extraction_Types.SCAV => Orange,
                Extraction_Types.SHARED => Blue,
                Extraction_Types.TRANSIT => Red,
                Extraction_Types.HIDDEN => Purple,
                _ => White
            };
        }
    }
}
