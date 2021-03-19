using Music_Player.Models;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class SearchViewModel {

    public List<Track> Tracks { get; private set; }

    public void Search(string text) {
      if (text == string.Empty) {
        this.Tracks = new List<Track>();
        return;
      }

      var genres = GenreList.Instance.Where(g => g.Name.Contains(text)).ToList();
      var songs = TrackList.Instance.Where(t =>
      t.Title.ToLower().Contains(text)
      || t.ArtistString.ToLower().Contains(text)
      || t.GenreString.ToLower().Contains(text)
      ).ToList();

      this.Tracks = songs;
    }
  }
}