namespace Music_Player.Models {
  public class Artist : ADisplayGroup {

    public const char SEPARATOR = '&';

    public Artist(string name, Track track) : base (name, track) { }

  }
}
