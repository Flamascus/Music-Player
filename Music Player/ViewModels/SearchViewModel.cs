using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class SearchViewModel {

    public List<ITrack> Tracks { get; private set; }

    public void Search(string text) {
      var logic = MainLogic.Instance;

      if (text == string.Empty) {
        this.Tracks = logic.AllTracks;
        return;
      }

      var genres = logic.AllGenres.Where(g => g.GenreName.Contains(text)).ToList();
      var songs = logic.AllTracks.Where(t =>
      t.Title.ToLower().Contains(text)
      || t.Producer.ToLower().Contains(text)
      || t.CombinedGenreName.ToLower().Contains(text)
      ).ToList();

      this.Tracks = songs;
    }
  }
}