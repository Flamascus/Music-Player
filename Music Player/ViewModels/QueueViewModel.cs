using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  public class QueueViewModel : ANotifyPropertyChanged {

    public ITrack CurrentTrack => this._queue.CurrentTrack;

    private readonly TrackQueue _queue;

    public List<ITrack> NextUpTracks => _queue.NextUpTracks;
    public List<ITrack> QueuedTracks {
      get {
        if (_queue.QueuedTracks.Count <= 20)
          return _queue.QueuedTracks;
        else
          return _queue.QueuedTracks.GetRange(0, 20);
      }
    }


    public QueueViewModel() {
      var queue = MainLogic.Instance.TrackQueue;
      this._queue = queue;

      queue.NewSongSelected += (sender, args) => this.OnPropertyChanged(nameof(CurrentTrack));
    }

  }
}
