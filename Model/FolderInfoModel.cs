using System.Collections;
using UM = FoldersVersionsManager.UtilManager;

namespace FoldersVersionsManager.Model
{
    public class FolderInfoModel : InfoModelBase, IEnumerable<InfoModelBase>
    {
        private List<InfoModelBase> Children { get; set; }
        public override string FullName => UM.FolderMarker + Name;

        public int Count => Children.Count;
        public int DeepCount
        {
            get
            {
                int count = 1;
                foreach (InfoModelBase info in Children)
                {
                    if (info is FolderInfoModel fInfo)
                    {
                        count += fInfo.DeepCount;
                    }
                    else if (info is FileInfoModel iInfo)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public long Weight => Children.Sum(x => x is FileInfoModel iInfo ? iInfo.Weight : 0);
        public override long DeepWeight => Children.Sum(x => x.DeepWeight);

        public void Add(InfoModelBase child) => Children.Add(child);

        internal FolderInfoModel(DirectoryInfo info, int inheritLvl, FolderInfoModel parent)
            : this(info.Name, inheritLvl, parent) { }
        
        internal FolderInfoModel(string name, int inheritLvl, FolderInfoModel parent)
            : base(name, inheritLvl, parent)
            => Children = new();

        public override string GetSavableText()
        {
            string result = UM.IndentMarker.Extend(InheritenceLevel) + UM.FolderMarker + Name + Environment.NewLine;
            Children.ForEach(x => result += x.GetSavableText());
            return result;
        }

        public static FolderInfoModel LoadFromText(string data)
            => Parse(data.Split(Environment.NewLine).ToList(), 0, null);
        private static FolderInfoModel Parse(List<string> data, int inheritLvl, FolderInfoModel caller)
        {
            string rootData = data[0];
            FolderInfoModel root = new(rootData.Trim('\t')[1..], inheritLvl, caller);

            List<string> innerBody = new();
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i].Count(x => x == '\t') >= inheritLvl + 1)
                    innerBody.Add(data[i]);
                else break;
            }

            List<InfoModelBase> children = new();
            while (innerBody.Count > 0)
            {
                string line = innerBody[0].Trim('\t');
                if (line.StartsWith(UM.FolderMarker))
                {
                    FolderInfoModel child = Parse(innerBody, inheritLvl + 1, root);
                    children.Add(child);
                    innerBody.RemoveRange(0, child.DeepCount);
                }
                else
                {
                    children.Add(FileInfoModel.Parse(line, inheritLvl + 1, root));
                    innerBody.RemoveAt(0);
                }
            }
            root.Children = children;

            return root;
        }

        public IEnumerator<InfoModelBase> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
