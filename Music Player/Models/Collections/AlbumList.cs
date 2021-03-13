using Music_Player.Helpers;
using Music_Player.Models.DisplayGroup;

namespace Music_Player.Models.Collections {
  public class AlbumList : LoadableList<Album> {

    public static readonly AlbumList Instance = new AlbumList();
    private AlbumList() { }

  }
}
