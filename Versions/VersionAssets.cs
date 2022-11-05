using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = FoldersVersionsManager.Versions.VersionManager;

namespace FoldersVersionsManager.Versions
{
    public class VersionAssets
    {
        public string AssetsName { get; set; } = "Default Assets";

        public string VersionMarker { get; set; } = "v";
        public int VersionStackSize { get; set; } = 4;
        public string DefaultVersionName { get; set; } = "Name";

        public VersionAssets() { }

        public string GetFileNameFor(Version v)
            => VersionMarker + '0'.Extend(VersionStackSize - v.VersionNumber.ToString().Length)
            + v.VersionNumber.ToString() + $".{v.Name}" + VM.SFExtension;
    }
}
