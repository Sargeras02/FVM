using Version = FoldersVersionsManager.Versions.Version;

namespace FoldersVersionsManager.Model
{
    internal class InfoModelManager
    {
        private FVManager Ownner { get; set; }

        public InfoModelManager(FVManager ownner)
        {
            Ownner = ownner;
        }

        public FolderInfoModel GetFolderIM(string path, int inheritLvl, FolderInfoModel caller)
        {
            FolderInfoModel root = new(new DirectoryInfo(path), inheritLvl, caller);

            Directory.GetDirectories(path)
                .ToList()
                .ForEach(x => root.Add(GetFolderIM(x, inheritLvl + 1, root)));

            Directory.GetFiles(path)
                .ToList()
                .ForEach(x => root.Add(GetFileIM(x, inheritLvl + 1, root)));

            return root;
        }

        public FileInfoModel GetFileIM(string path, int inheritLvl, FolderInfoModel caller)
            => new(new FileInfo(path), inheritLvl, caller);

        public void SaveAs(FolderInfoModel model, Version version)
        {
            using StreamWriter sw = new StreamWriter(Ownner.SavePath + $"/{version.FileName}");
            sw.Write(model.GetSavableText());
        }

        public FolderInfoModel? GetVersionData(Version? version)
            => version != null && File.Exists(Ownner.SavePath + $"/{version.FileName}")
            ? FolderInfoModel.LoadFromText(File.ReadAllText(Ownner.SavePath + $"/{version.FileName}"))
            : null;
    }
}
