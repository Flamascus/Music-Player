using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models {
  public abstract class ADisplayGroup : IDisplayGroup {
    public string Name { get; }
    public int TrackAmount => this.Tracks.Count;
    public string TrackAmountUi => this.TrackAmount + " Songs";
    public List<Track> Tracks { get; }

    protected ADisplayGroup(string name, Track track) {
      this.Name = name;
      this.Tracks = new List<Track> { track };
    }

    public ADisplayGroup(string name, List<Track> tracks) {
      this.Name = name;
      this.Tracks = tracks;
    }

    public override string ToString() => this.Name;
   
  }
}
