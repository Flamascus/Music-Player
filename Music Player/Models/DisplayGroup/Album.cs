using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models.DisplayGroup {
  public class Album : ADisplayGroup {
    public Album(string name, List<ITrack> tracks) : base(name, tracks) { }
  }
}
