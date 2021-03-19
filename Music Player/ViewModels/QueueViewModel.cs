using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class QueueViewModel : ANotifyPropertyChanged {

    public Track CurrentTrack => this._queue.CurrentTrack;

    private readonly TrackQueue _queue;

    //todo: need to find better way than just copying the lists
    public List<Track> NextUpTracks => new List<Track>(this._queue.NextUpTracks);
    public List<Track> QueuedTracks {
      get {
        return this._queue.QueuedTracks.Count <= 20
          ? new List<Track>(this._queue.QueuedTracks)
          : this._queue.QueuedTracks.GetRange(0, 20);
      }
    }

    public bool NextUpsVisible => this.NextUpTracks.Any();
    public bool QueuedVisible => this.QueuedTracks.Any();

    public QueueViewModel() {
      var queue = TrackQueue.Instance;
      this._queue = queue;

      queue.NewSongSelected += (sender, args) => this.Refresh();
    }

    public void Refresh() {
      this.OnPropertyChanged(nameof(this.CurrentTrack));
      this.OnPropertyChanged(nameof(this.NextUpTracks));
      this.OnPropertyChanged(nameof(this.QueuedTracks));
      this.OnPropertyChanged(nameof(this.NextUpsVisible));
      this.OnPropertyChanged(nameof(this.QueuedVisible));
    }

  }
}
