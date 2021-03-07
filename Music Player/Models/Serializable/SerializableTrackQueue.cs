using Music_Player.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models.Serializable {
  public class SerializableTrackQueue {

    public string CurrentTrackPath { get; }
    public TimeSpan Progress { get; }
    public string[] QueuedTracksPaths { get; }
    public string[] NextUpTracksPaths { get; }

    public SerializableTrackQueue(string currentTrackPath, TimeSpan progress, string[] queuedTracksPaths, string[] nextUpTracksPaths) {
      this.CurrentTrackPath = currentTrackPath;
      this.Progress = progress;
      this.QueuedTracksPaths = queuedTracksPaths;
      this.NextUpTracksPaths = nextUpTracksPaths;
    }

    public static SerializableTrackQueue Create() {
      var queue = TrackQueue.Instance;

      return new SerializableTrackQueue(
        queue.CurrentTrack.Path,
        queue.CurrentTrack.GetProgress(),
        queue.QueuedTracks.Select(t => t.Path).ToArray(),
        queue.NextUpTracks.Select(t => t.Path).ToArray()
        );
    }

    public void SetTrackQueue() {
      var queue = TrackQueue.Instance;
      var nextUps = this._CreateTrackList(this.NextUpTracksPaths);
      var queued = this._CreateTrackList(this.QueuedTracksPaths);


      queue.FullyCreateQueue(nextUps, queued, TrackList.Instance.FirstOrDefault(t => t.Path == this.CurrentTrackPath));
      queue.CurrentTrack.SetProgress(this.Progress);
    }

    private List<ITrack> _CreateTrackList(string[] paths) {
      var tracks = new List<ITrack>();

      foreach (var path in paths) {
        var song = TrackList.Instance.FirstOrDefault(t=> t.Path == path);
        if (song != null)
          tracks.Add(song);
      }

      return tracks;
    }
  }
}
