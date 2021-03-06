using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  public class QueueViewModel : ANotifyPropertyChanged {

    public ITrack CurrentTrack => this._queue.CurrentTrack;

    private readonly TrackQueue _queue;

    //todo: need to find better way than just copying the lists
    public List<ITrack> NextUpTracks => new List<ITrack>(this._queue.NextUpTracks);
    public List<ITrack> QueuedTracks {
      get {
        if (this._queue.QueuedTracks.Count <= 20)
          return new List<ITrack>(this._queue.QueuedTracks);
        else
          return this._queue.QueuedTracks.GetRange(0, 20);
      }
    }

    public QueueViewModel() {
      var queue = TrackQueue.Instance;
      this._queue = queue;

      queue.NewSongSelected += (sender, args) => {
        this.OnPropertyChanged(nameof(this.CurrentTrack));
        this.OnPropertyChanged(nameof(this.NextUpTracks));
        this.OnPropertyChanged(nameof(this.QueuedTracks));    
      };
    }

  }
}
