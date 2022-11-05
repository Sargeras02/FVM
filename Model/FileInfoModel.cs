using UM = FoldersVersionsManager.UtilManager;

namespace FoldersVersionsManager.Model
{
    public class FileInfoModel : InfoModelBase
    {
        public override string FullName => Name + Extension;

        public string Extension { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime EditedOn { get; private set; }

        public long Weight { get; private set; }
        public override long DeepWeight => Weight;

        internal FileInfoModel(FileInfo info, int inheritLvl, FolderInfoModel parent)
            : this(info.Name, inheritLvl, parent)
        {
            Weight = info.Length;
            CreatedOn = info.CreationTime;
            EditedOn = info.LastWriteTime;
        }
        internal FileInfoModel(string name, int inheritLvl, FolderInfoModel parent)
            : base(name, inheritLvl, parent)
        {
            Extension = name.Contains('.')
                ? name[name.IndexOf('.')..]
                : string.Empty;
        }

        public override string GetSavableText()
            => UM.IndentMarker.Extend(InheritenceLevel) + Name
            + UM.SeparatorMarker + Weight
            + UM.SeparatorMarker + CreatedOn
            + UM.SeparatorMarker + EditedOn
            + Environment.NewLine;

        public static FileInfoModel Parse(string data, int inheritLvl, FolderInfoModel caller)
        {
            string[] parts = data.Split(UM.SeparatorMarker);
            return new(parts[0], inheritLvl, caller)
            {
                Weight = long.Parse(parts[1]),
                CreatedOn = DateTime.Parse(parts[2]),
                EditedOn = DateTime.Parse(parts[3]),
            };
        }
    }
}
