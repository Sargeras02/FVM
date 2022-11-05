using FoldersVersionsManager.Model;

namespace FoldersVersionsManager.Comparison
{
    public abstract class ComparisonBase
    {
        public abstract string FullName { get; }
        public abstract int InheritenceLevel { get; }

        public static FolderComparison Compare(FolderInfoModel? old, FolderInfoModel? @new) => new(old, @new);
        public static FileComparison Compare(FileInfoModel? old, FileInfoModel? @new) => new(old, @new);

        public override string ToString() => FullName;
    }
}
