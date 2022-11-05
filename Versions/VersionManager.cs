using System.Collections;

namespace FoldersVersionsManager.Versions
{
    internal class VersionManager : IEnumerable<Version>
    {
        public static string SFExtension => ".fver";

        private FVManager Owner { get; set; }
        public string SavePath => Owner.SavePath;
        public VersionAssets Assets => Owner.Assets;

        private List<Version> Versions { get; set; }

        public VersionManager(FVManager owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Versions = new();
            Directory.GetFiles(SavePath, $"*{SFExtension}")
                .ToList()
                .TruncPathAndOrder()
                .ForEach(x => Add(new Version(Assets, x)));
        }

        public void Add(Version version) => Versions.Add(version);

        public Version? GetVersion(int vNum) => Versions.Find(x => x.VersionNumber == vNum);
        public string? GetVersionFile(int vNum) => GetVersion(vNum)?.FileName;

        public int GetLastVersionNum()
            => Versions.Count <= 0 ? -1
            : Versions.Max(x => x.VersionNumber);
        public Version? GetLastVersion()
            => Versions.Count <= 0 ? null
            : Versions.Find(x => x.VersionNumber == GetLastVersionNum());
        public string? GetLastVersionFile() => GetVersionFile(GetLastVersionNum());

        public int GetNewVersionNum() => GetLastVersionNum() + 1;
        public Version GetNewVersion() => new(Assets, GetNewVersionNum());

        public IEnumerator<Version> GetEnumerator() => Versions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
