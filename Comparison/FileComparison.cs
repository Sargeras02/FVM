using FoldersVersionsManager.Model;
using UM = FoldersVersionsManager.UtilManager;

namespace FoldersVersionsManager.Comparison
{
    public class FileComparison : ComparisonBase
    {
        public FileInfoModel? Old { get; set; }
        public FileInfoModel? New { get; set; }

        public long WeightDiffer => (New?.Weight ?? 0) - (Old?.Weight ?? 0);
        public bool IsOverriden => Old != null && New != null && UM.IsEqualDate(Old.CreatedOn, New.CreatedOn);
        public bool IsEdited => Old != null && New != null && UM.IsEqualDate(Old.EditedOn, New.EditedOn);

        public bool IsSimilar =>  IsEdited && IsOverriden && WeightDiffer == 0;
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

        public FileComparison(FileInfoModel? old, FileInfoModel? @new)
        {
            Old = old;
            New = @new;
        }
    }
}
