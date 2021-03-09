using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models.DisplayGroup {
  public class Playlist : ADisplayGroup {
    public Playlist(string name, List<ITrack> tracks) : base(name, tracks) { }
  }
}