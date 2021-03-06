using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class SearchViewModel {

    public List<ITrack> Tracks { get; private set; }

    public void Search(string text) {
      if (text == string.Empty) {
        this.Tracks = new List<ITrack>();
        return;
      }

      var genres = GenreList.Instance.Where(g => g.Name.Contains(text)).ToList();
      var songs = TrackList.Instance.Where(t =>
      t.Title.ToLower().Contains(text)
      || t.CombinedArtistNames.ToLower().Contains(text)
      || t.CombinedGenreName.ToLower().Contains(text)
      ).ToList();

      this.Tracks = songs;
    }
  }
}