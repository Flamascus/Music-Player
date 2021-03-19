using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Music_Player.Models.DisplayGroup {
  public class Folder : ALoadableDisplayGroup {

    //todo: put these 2 in extra class maybe
    public static Folder Root { get; set; }
    public static List<Folder> AllFolders { get; } = new List<Folder>();

    //todo: only for debug, implement better
    public static bool IsLoading { get; set; } = true;

    public DirectoryInfo Directory { get; }

    public Folder[] SubFolders { get; }

    public Folder(DirectoryInfo directory) : base() {
      this.Directory = directory;
      this.SubFolders = directory.EnumerateDirectories().Select(d => new Folder(d)).ToArray();

      if (this.SubFolders == null)
        this.SubFolders = new Folder[0];

      AllFolders.Add(this);
    }

    public override string ToString() => this.Directory.Name;
  }
}