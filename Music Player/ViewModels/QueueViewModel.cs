using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  public class QueueViewModel : ANotifyPropertyChanged {

    public ITrack CurrentTrack => this._queue.CurrentTrack;

    private readonly TrackQueue _queue;

    //todo: need to find better way than just copying the lists
    public List<ITrack> NextUpTracks => new List<ITrack>(_queue.NextUpTracks);
    public List<ITrack> QueuedTracks {
      get {
        if (_queue.QueuedTracks.Count <= 20)
          return new List<ITrack>(_queue.QueuedTracks);
        else
          return _queue.QueuedTracks.GetRange(0, 20);
      }
    }

    public QueueViewModel() {
      var queue = TrackQueue.Instance;
      this._queue = queue;

      queue.NewSongSelected += (sender, args) => {
        this.OnPropertyChanged(nameof(CurrentTrack));
        this.OnPropertyChanged(nameof(NextUpTracks));
        this.OnPropertyChanged(nameof(QueuedTracks));    
      };
    }

    public void JumpToClickedTrack(ITrack track, List<ITrack> tracks) {
      var queue = TrackQueue.Instance;

      for (var i = 0; i < tracks.Count; ++i) {
        if (tracks[i] == track) {
          tracks.RemoveRange(0, i + 1);
          queue.Play(track);       
          break;
        }
      }
    }

  }
}
