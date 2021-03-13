using Music_Player.Droid.Classes;
using Music_Player.Interfaces;

namespace Music_Player.Models.DisplayGroup {
  public class Album : ADisplayGroup, IDisplayGroup {

    public Album(string name, Track track) : base(name, track) { }

  }
}
