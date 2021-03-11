using Music_Player.Helpers;
using Music_Player.Models.DisplayGroup;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models.Collections {
  public class AlbumList : AReadOnlyList<Album> {

    public static readonly AlbumList Instance = new AlbumList();

    private AlbumList() { }

    public void Init() {
      var tracks = TrackList.Instance;
      var albums = new List<Album>();

      foreach (var track in tracks) {
        var album = albums.FirstOrDefault(a => a.Name == track.Album);

        if (album == null)
          albums.Add(new Album(track.Album, track));
        else
          album.Tracks.Add(track);

      }
      albums.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));

      this.items = albums;
      this.IsLoading = false;
    }


  }
}
