using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  class SongsViewModel : ANotifyPropertyChanged {
    private List<ITrack> _tracks;

    public List<ITrack> Tracks {
      get => this._tracks;
      set {
        this.OnPropertyChanged();
        this._tracks = value;
      }
    }

    public SongsViewModel() {
      this.Tracks = TrackList.Instance.ToList();
    }

    public SongsViewModel(List<ITrack> tracks) {
      this.Tracks = tracks;
    }

    public void OnTrackTapped(ITrack track) {
      var trackQueue = TrackQueue.Instance;
      var queue = new List<ITrack>();
      var tracks = this.Tracks;
      var index = tracks.IndexOf(track);

      for (var i = index; i < tracks.Count; ++i)
        queue.Add(tracks[i]);

      trackQueue.ChangeQueue(queue);
      trackQueue.Play();
    }
  }
}