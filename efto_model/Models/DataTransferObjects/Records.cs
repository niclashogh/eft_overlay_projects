using efto_model.Models.Enums;
using System.Drawing;

namespace efto_model.Models.DataTransferObjects
{
    public readonly record struct MetadataRecord(string Creator, string Version, string UpdatedToGameVersion);

    public readonly record struct PositionRecord<H, V>(H HorizontalPlacement, V VerticalPlacement);
    public readonly record struct DimensionRecord<T>(T Width, T Height);

    public readonly record struct ViewRecord<T>(T View, string Title);
}
