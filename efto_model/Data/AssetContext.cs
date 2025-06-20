namespace efto_model.Data
{
    public static class AssetContext
    {
        private static string roamingFolder { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ApplicationFolder { get; } = CreateFolder();

        private static string CreateFolder()
        {
            string path = Path.Combine(roamingFolder, "EFT Overlay", "Assets");
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
