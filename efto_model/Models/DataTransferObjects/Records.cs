using System.Drawing;

namespace efto_model.Models.DataTransferObjects
{
    public readonly record struct MetadataRecord(string Creator, string Version, string UpdatedToGameVersion);

    public readonly record struct PositionRecord<H, V>(H HorizontalPlacement, V VerticalPlacement);
    public readonly record struct DimensionRecord<T>(T Width, T Height);

    public readonly record struct ViewRecord<T>(T View, string Title);

    public readonly record struct ColorPalette()
    {
        public List<Color> Palette { get; } = new List<Color>
        {
            Color.White,
            Color.FromArgb(55, 84, 169),
            Color.FromArgb(169, 55, 55),
            Color.FromArgb(92, 120, 91),
            Color.FromArgb(180, 123, 38),
            Color.FromArgb(76, 69, 140),
            Color.FromArgb(169, 160, 55),
            Color.FromArgb(35, 160, 141),
            Color.FromArgb(160, 18, 132),
            Color.FromArgb(84, 67, 47)
        };
    }
}
