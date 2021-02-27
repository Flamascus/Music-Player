using System.Collections.Generic;

namespace Music_Player.Interfaces {
  public interface IDisplayGroup {
    string Name { get; }
    int TrackAmount { get; }
    string TrackAmountUi { get; }
    List<ITrack> Tracks { get; }
  }
}
