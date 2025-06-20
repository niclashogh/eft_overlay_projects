namespace efto_model.Records
{
    public readonly record struct ViewRecord<T>(T View, string Title);
}
