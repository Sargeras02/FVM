namespace FoldersVersionsManager.Model
{
    public abstract class InfoModelBase
    {
        public bool IsChecked { get; private set; } = false;
        public void Check() => IsChecked = true;

        public string Name { get; private set; }
        public FolderInfoModel Parent { get; private set; }
        public int InheritenceLevel { get; private set; }

        public abstract string FullName { get; }

        public abstract long DeepWeight { get; }

        internal InfoModelBase(string name, int inheritLvl, FolderInfoModel parent, bool isFolder = true)
        {
            InheritenceLevel = inheritLvl;
            Parent = parent;

            Name = !isFolder && name.Contains('.')
                ? name[..name.IndexOf('.')]
                : name;
        }

        public abstract string GetSavableText();
        public override string ToString() => FullName;
    }
}
