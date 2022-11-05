using FoldersVersionsManager.Comparison;
using UM = FoldersVersionsManager.UtilManager;

namespace FoldersVersionsManager.Output
{
    public static class Logger
    {
        public static void Log(FolderComparison? result)
        {
            if (result == null)
            {
                Console.WriteLine("No Comparison Model");
                return;
            }

            if (result.IsSimilar)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[ ] " + result.FullName);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                if (result.IsNew)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[+] " + result.FullName);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (result.IsDeleted)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[-] " + result.FullName);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[~] " + result.FullName);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                foreach (var compare in result.Children)
                {
                    if (compare is FolderComparison folderComp)
                    {
                        Log(folderComp);
                    }
                    else if (compare is FileComparison fileComp)
                    {
                        Log(fileComp);
                    }
                }
            }
        }
        private static void Log(FileComparison result)
        {
            if (result.IsSimilar)
                return;

            if (result.IsNew)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[+] " + result.FullName);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (result.IsDeleted)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine('\t'.Extend(result.InheritenceLevel) + "[-] " + result.FullName);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (result.IsEdited)
            {
                Console.Write('\t'.Extend(result.InheritenceLevel) + "[~] " + result.FullName + $" {UM.SeparatorMarker} ");
                if (result.WeightDiffer < 0)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (result.WeightDiffer == 0)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                string adInfo = $"{result.New!.Weight} bytes [{(result.WeightDiffer > 0 ? "+" : "")}{result.WeightDiffer} bytes]";
                Console.WriteLine(adInfo);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
