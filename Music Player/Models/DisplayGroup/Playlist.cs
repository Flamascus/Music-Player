using System.Collections.Generic;

namespace Music_Player.Models.DisplayGroup {
  public class Playlist : ADisplayGroup {
    public Playlist(string name, List<Track> tracks) : base(name, tracks) { }
  }
}