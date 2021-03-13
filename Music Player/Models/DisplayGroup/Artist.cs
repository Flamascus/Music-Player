using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models {
  public class Artist : ADisplayGroup {

    public Artist(string name, Track track) : base (name, track) { }

  }
}
