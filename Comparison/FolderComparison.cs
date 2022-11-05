using FoldersVersionsManager.Model;
using System.Security.Authentication;

namespace FoldersVersionsManager.Comparison
{
    public class FolderComparison : ComparisonBase
    {
        public FolderInfoModel? Old { get; private set; }
        public FolderInfoModel? New { get; private set; }

        public List<ComparisonBase> Children { get; private set; }

        public long WeightDiffer => (New?.Weight ?? 0) - (Old?.Weight ?? 0);
        public long DeepWeightDiffer => (New?.DeepWeight ?? 0) - (Old?.DeepWeight ?? 0);
        public int CountDiffer => (New?.Count ?? 0) - (Old?.Count ?? 0);
        public int DeepCountDiffer => (New?.DeepCount ?? 0) - (Old?.DeepCount ?? 0);

        public bool IsSimilar => DeepWeightDiffer == 0 && DeepCountDiffer == 0;
        public bool IsNew => Old == null;
        public bool IsDeleted => New == null;

        public override int InheritenceLevel
            => Old != null
            ? Old.InheritenceLevel
            : (New != null ? New.InheritenceLevel : -1);
        public override string FullName
            => Old != null
            ? Old.FullName
            : (New != null ? New.FullName : "[???]");

        public FolderComparison(FolderInfoModel? old, FolderInfoModel? @new)
        {
            Old = old;
            New = @new;
            Children = new();
            CompareFolders();
        }

        private void CompareFolders()
        {
            Dictionary<string, InfoModelBase> olds = new();
            Dictionary<string, InfoModelBase> news = new();

            if (Old != null)
                olds = Old.ToDictionary(x => x.FullName);
            if (New != null)
                news = New.ToDictionary(x => x.FullName);

            // Поиск различий новой версии от старой
            foreach (var pair in news)
            {
                InfoModelBase info = pair.Value;
                if (info is FolderInfoModel fInfo)
                {
                    string folderName = fInfo.FullName;
                    FolderInfoModel? fOld = olds.ContainsKey(folderName)
                        && olds[folderName] is FolderInfoModel tOld
                            ? tOld : null;

                    Children.Add(Compare(fOld, fInfo));
                    fInfo.Check();
                    fOld?.Check();
                }
                else if (info is FileInfoModel iInfo)
                {
                    string fileName = iInfo.FullName;
                    FileInfoModel? iOld = olds.ContainsKey(fileName)
                        && olds[fileName] is FileInfoModel tOld
                        ? tOld : null;

                    Children.Add(Compare(iOld, iInfo));
                    iOld?.Check();
                    iInfo.Check();
                }
            }

            Dictionary<string, InfoModelBase> oldRest = olds.Values.Where(x => !x.IsChecked).ToDictionary(x => x.FullName);
            foreach (var pair in oldRest)
            {
                InfoModelBase info = pair.Value;
                if (info is FolderInfoModel fInfo)
                {
                    Children.Add(Compare(fInfo, null));
                }
                else if (info is FileInfoModel iInfo)
                {
                    Children.Add(Compare(iInfo, null));
                }
                info.Check();
            }
        }
    }
}
