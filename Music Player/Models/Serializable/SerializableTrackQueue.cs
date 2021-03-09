using System;
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

      if (queue.CurrentTrack == null)
        return null;

      return new SerializableTrackQueue(
        queue.CurrentTrack.Path,
        queue.CurrentTrack.GetProgress(),
        queue.QueuedTracks.Select(t => t.Path).ToArray(),
        queue.NextUpTracks.Select(t => t.Path).ToArray()
        );
    }

    public void SetTrackQueue() {
      var queue = TrackQueue.Instance;
      var nextUps = Helpers.Helpers.CreateTracklistFromPaths(this.NextUpTracksPaths);
      var queued = Helpers.Helpers.CreateTracklistFromPaths(this.QueuedTracksPaths);
      var currentTrack = TrackList.Instance.FirstOrDefault(t => t.Id == this.CurrentTrackPath.GetHashCode());

      if (currentTrack == null)
        return;

      queue.FullyCreateQueue(nextUps, queued, currentTrack);
      queue.CurrentTrack.SetProgress(this.Progress);
    }
  }
}
