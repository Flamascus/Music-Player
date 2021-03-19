using Music_Player.Helpers;
using Music_Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class SongsViewModel : ALoadableNotifyPropertyChanged {

    private List<Track> _tracks;

    public List<Track> Tracks {
      get => this._tracks;
      set {
        this.OnPropertyChanged();
        this._tracks = value;
      }
    }

  public SongsViewModel() {
      this.Tracks = TrackList.Instance.ToList();
      TrackList.Instance.StartedLoading += (_, __) => this.IsLoading = true;
      TrackList.Instance.FinishedLoading += this._Instance_FinishedLoading;

      if (TrackList.Instance.IsLoading) {
        this.IsLoading = true;
        
      }
    }

    //todo: check why this still gets fired twice
    private void _Instance_FinishedLoading(object sender, EventArgs e) {
      this.Tracks = TrackList.Instance.ToList();
      this.IsLoading = false;
    }

    public SongsViewModel(List<Track> tracks) {
      this.Tracks = tracks;
    }

    public void OnTrackTapped(Track track) {
      var trackQueue = TrackQueue.Instance;
      var queue = new List<Track>();
      var tracks = this.Tracks;
      var index = tracks.IndexOf(track);

      for (var i = index; i < tracks.Count; ++i)
        queue.Add(tracks[i]);

      trackQueue.ChangeQueue(queue);
      trackQueue.Play();
    }
  }
}