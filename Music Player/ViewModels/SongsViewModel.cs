using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Music_Player.ViewModels {
  class SongsViewModel : INotifyPropertyChanged {
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

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnTrackTapped(ITrack track) {
      //MainLogic.Instance.CurrentTrack = track;
      var logic = MainLogic.Instance;
      var queue = new Queue<ITrack>();
      var tracks = this.Tracks;
      var index = tracks.IndexOf(track);

      for (var i = index; i < tracks.Count; ++i)
        queue.Enqueue(tracks[i]);

      logic.TrackQueue = queue;
      logic.Play();
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}