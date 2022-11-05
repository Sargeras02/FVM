using FoldersVersionsManager.Comparison;
using FoldersVersionsManager.Model;
using FoldersVersionsManager.Versions;
using Version = FoldersVersionsManager.Versions.Version;

namespace FoldersVersionsManager
{
    public class FVManager
    {
        public string WorkDirectory { get; private set; }
        public string SavePath { get; private set; }

        private InfoModelManager IMManager { get; set; }
        private ComparisonManager ComparisonManager { get; set; }
        private VersionManager VersionManager { get; set; }
        internal VersionAssets Assets { get; set; }

        public FVManager(string workDirectory, string savePath)
        {
            WorkDirectory = workDirectory;
            SavePath = savePath;

            // TODO: implict config load
            Assets = new();

            IMManager = new(this);
            ComparisonManager = new(this);
            VersionManager = new(this);
        }

        #region Info Models
        public FolderInfoModel GetInfoModel() => IMManager.GetFolderIM(WorkDirectory, 0, null);
        public void Save(FolderInfoModel model)
        {
            Version newVersion = VersionManager.GetNewVersion();
            VersionManager.Add(newVersion);
            SaveAs(model, newVersion);
        }
        public void SaveAs(FolderInfoModel model, Version version) => IMManager.SaveAs(model, version);
        public FolderInfoModel? GetVersionData(Version? version) => IMManager.GetVersionData(version);
        #endregion

        #region Comprasions
        public FolderComparison Compare(FolderInfoModel old, FolderInfoModel @new)
            => ComparisonManager.Compare(old, @new);
        #endregion

        #region Version Manager
        public Version? GetVersion(int vNum) => VersionManager.GetVersion(vNum);
        public string? GetVersionFile(int vNum) => VersionManager.GetVersionFile(vNum);
        public Version? GetLastVersion() => VersionManager.GetLastVersion();
        #endregion
    }
}
