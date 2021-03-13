using Music_Player.Helpers;

namespace Music_Player.Models {
  public class GenreList : LoadableList<Genre> {

    public static GenreList Instance = new GenreList();
    private GenreList() { }

  }
}
