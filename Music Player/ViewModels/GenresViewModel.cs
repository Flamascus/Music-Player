using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  class GenresViewModel {

    public IList<Genre> Genres { get; }

    public GenresViewModel() {
      this.Genres = MainLogic.Instance.AllGenres;
    }

    public void OnGenreTapped(Genre genre) {

    }
  }
}
