using System.Runtime.CompilerServices;

namespace FoldersVersionsManager
{
    internal static class UtilManager
    {
        public static char IndentMarker => '\t';
        public static char FolderMarker => '/';
        public static char SeparatorMarker => '|';

        public static string Extend(this char @base, int count)
        {
            string res = string.Empty;
            for (int i = 0; i < count; i++)
                res += @base;
            return res;
        }

        public static List<string> TruncPathAndOrder(this List<string> source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                string path = source[0];
                if (path.Contains('\\'))
                    source.Add(path[(path.LastIndexOf('\\') + 1)..]);
                else
                    source.Add(path);
                source.RemoveAt(0);
            }
            return source.OrderByDescending(x => x).ToList();
        }

        public static bool IsEqualDate(DateTime first, DateTime second)
            => first.Second == second.Second
            && first.Minute == second.Minute
            && first.Hour == second.Hour
            && first.Day == second.Day
            && first.Month == second.Month
            && first.Year == second.Year;
    }
}
