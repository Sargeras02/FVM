namespace FoldersVersionsManager.Versions
{
    public class Version
    {
        public VersionAssets Assets { get; private set; }

        public int VersionNumber { get; private set; }
        public string Name { get; private set; }
        public string FileName { get; private set; }

        public Version(VersionAssets assets, int versionNumber, string? name = null)
        {
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            VersionNumber = versionNumber;
            Name = name ?? Assets.DefaultVersionName;
            FileName = Assets.GetFileNameFor(this);
        }   

        public Version(VersionAssets assets, string fileName)
        {
            Assets = assets;
            FileName = fileName;

            VersionNumber = int.TryParse(fileName[Assets.VersionMarker.Length..fileName.IndexOf('.')], out int res)
                ? res
                : throw new ArgumentException("Version Number Parse Error");
            Name = fileName.Split('.').Length > 1
                ? fileName.Split('.')[1]
                : throw new ArgumentException("Version Name Parse Error");
        }

        public override string ToString() => FileName;
    }
}
