using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models {
  public class Artist : ADisplayGroup {

    public Artist(string name, List<ITrack> tracks) : base(name, tracks) { }
  }
}
