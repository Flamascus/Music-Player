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
    public Folder Parent { get; }

    public Folder(DirectoryInfo directory, Folder parent = null) : base() {
      this.Directory = directory;
      this.Parent = parent;
      this.SubFolders = directory.EnumerateDirectories().Select(d => new Folder(d, this)).ToArray();

      if (this.SubFolders == null) //todo: dont think this is needed
        this.SubFolders = new Folder[0];

      AllFolders.Add(this);
    }

    public override string ToString() => this.Directory.Name;
  }
}