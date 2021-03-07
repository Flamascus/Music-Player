using Music_Player.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models {
  public class GenreList : AReadOnlyList<Genre> {

    public static GenreList Instance = new GenreList();
    private GenreList() { }

    public void Init() {
      var tracks = TrackList.Instance;
      var allGenreNames = new List<string>();

      foreach (var track in tracks)
        foreach (var genre in track.GenreNames) {
          if (!allGenreNames.Any(g => g.Equals(genre, StringComparison.OrdinalIgnoreCase)))
            allGenreNames.Add(genre);
        }

      allGenreNames = allGenreNames.ToList();
      allGenreNames.Sort();
      var genres = allGenreNames.Select(g => new Genre(g, tracks.Where(t => t.GenreNames.Contains(g)).ToList())).ToList();

      this.items = genres;
      this.IsLoading = false;
    }

  }
}
