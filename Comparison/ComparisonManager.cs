using FoldersVersionsManager.Model;

namespace FoldersVersionsManager.Comparison
{
    internal class ComparisonManager
    {
        public FVManager Owner { get; set; }

        public ComparisonManager(FVManager owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public FolderComparison Compare(FolderInfoModel old, FolderInfoModel @new)
        {
            return new FolderComparison(old, @new);
        }
    }
}
