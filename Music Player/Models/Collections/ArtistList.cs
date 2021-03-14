using Music_Player.Helpers;

namespace Music_Player.Models {
  public class ArtistList : LoadableList<Artist> {

    public static ArtistList Instance = new ArtistList();
    private ArtistList() { }

  }
}
