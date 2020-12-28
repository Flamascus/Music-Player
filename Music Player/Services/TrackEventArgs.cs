using Music_Player.Interfaces;
using System;

namespace Music_Player.Services {
  public class TrackEventArgs : EventArgs {
    public ITrack Track { get; }

    public TrackEventArgs(ITrack track) {
      this.Track = track;
    }
  }
}
