using System.IO;

namespace Music_Player.Models {
  public class SystemFile {

    public FileSystemInfo Info { get; }
    public string Name => this.Info.Name;
    public string Icon => this.Info is DirectoryInfo ? "folder.png" : "music_file.png";

    public SystemFile(FileSystemInfo info) {
      this.Info = info;
    }
  }
}
