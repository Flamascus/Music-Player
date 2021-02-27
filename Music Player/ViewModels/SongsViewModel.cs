using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  class SongsViewModel : ANotifyPropertyChanged {
    private List<ITrack> _tracks;

    public List<ITrack> Tracks {
      get => _tracks;
      set {
        this.OnPropertyChanged();
        _tracks = value;
      }
    }

    public SongsViewModel() {
      this.Tracks = MainLogic.Instance.AllTracks;
    }

    public SongsViewModel(List<ITrack> tracks) {
      this.Tracks = tracks;
    }

    public void OnTrackTapped(ITrack track) {
      //MainLogic.Instance.CurrentTrack = track;
      var trackQueue = MainLogic.Instance.TrackQueue;
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