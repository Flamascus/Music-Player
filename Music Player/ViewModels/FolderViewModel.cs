using Music_Player.Models;
using Music_Player.Models.DisplayGroup;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  public class FolderViewModel {

    public Folder Folder { get; set; }

    public Folder[] Folders => this.Folder.SubFolders;
    public List<Track> Tracks => this.Folder.Tracks;

    public FolderViewModel() {
      this.Folder = Folder.Root;
    }

  }
}
