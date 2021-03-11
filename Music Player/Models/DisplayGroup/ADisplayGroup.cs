using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.Models {
  public abstract class ADisplayGroup : IDisplayGroup {
    public string Name { get; }
    public int TrackAmount => this.Tracks.Count;
    public string TrackAmountUi => this.TrackAmount + " Songs";
    public List<ITrack> Tracks { get; }

    public ADisplayGroup(string name, ITrack track) {
      this.Name = name;
      this.Tracks = new List<ITrack> { track };
    }

    public ADisplayGroup(string name, List<ITrack> tracks) {
      this.Name = name;
      this.Tracks = tracks;
    }

    public override string ToString() => this.Name;
  }
}
