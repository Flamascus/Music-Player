using Music_Player.Models;
using System;

namespace Music_Player.Services {
  public class TrackEventArgs : EventArgs {
    public Track Track { get; }

    public TrackEventArgs(Track track) {
      this.Track = track;
    }
  }
}
