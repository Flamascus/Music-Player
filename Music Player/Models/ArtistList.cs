using Music_Player.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models {
  public class ArtistList : AReadOnlyList<Artist> {

    public static ArtistList Instance = new ArtistList();
    private ArtistList() { }

    public bool IsInitialized { get; set; } //todo: only for debug!

    public void Init() {
      var tracks = TrackList.Instance;
      var allArtistNames = new List<string>();

      foreach (var track in tracks)
        foreach (var artist in track.ArtistNames) {
          if (!allArtistNames.Any(g => g.Equals(artist, StringComparison.OrdinalIgnoreCase)))
            allArtistNames.Add(artist);
        }

      allArtistNames = allArtistNames.ToList();
      allArtistNames.Sort();
      var artists = allArtistNames.Select(g => new Artist(g, tracks.Where(t => t.ArtistNames.Contains(g)).ToList())).ToList();

      this.items = artists;
      this.IsInitialized = true;
    }
  }
}
