using efto_model.Models.Enums;
using System.Drawing;

namespace efto_model.Models.DataTransferObjects
{
    public readonly record struct MetadataRecord(string Creator, string Version, string UpdatedToGameVersion);

    public readonly record struct PositionRecord<H, V>(H HorizontalPlacement, V VerticalPlacement);
    public readonly record struct DimensionRecord<T>(T Width, T Height);

    public readonly record struct ViewRecord<T>(T View, string Title);

    public readonly record struct PaletteRecord()
    {
        public Dictionary<Palette, Color> Dictionary { get; } = new Dictionary<Palette, Color>
        {
            { Palette.White, Color.White },
            { Palette.Blue, Color.FromArgb(55, 84, 169) },
            { Palette.Red, Color.FromArgb(169, 55, 55) },
            { Palette.Green, Color.FromArgb(92, 120, 91) },
            { Palette.Orange, Color.FromArgb(180, 123, 38) },
            { Palette.Purple, Color.FromArgb(76, 69, 140) },
            { Palette.Yellow, Color.FromArgb(169, 160, 55) },
            { Palette.Teal, Color.FromArgb(35, 160, 141) },
            { Palette.Magenta, Color.FromArgb(160, 18, 132) },
            { Palette.Brown, Color.FromArgb(84, 67, 47) }
        };
    }
}
