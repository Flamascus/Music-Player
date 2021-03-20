using Music_Player.Enums;
using Music_Player.Models;
using Music_Player.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  public class SongsViewModel : ANotifyPropertyChanged {

    public List<Track> Tracks {
      get => this._tracks;
      set {     
        this._tracks = value;
        this.OnPropertyChanged();
      }
    }

    public DisplayState DisplayState {
      get => this._displayState;
      private set {
        this._displayState = value;
        this.OnPropertyChanged();
      }
    }

    private List<Track> _tracks;
    private DisplayState _displayState;

    public SongsViewModel() {
      this.Tracks = TrackList.Instance.ToList();
      TrackList.Instance.StartedLoading += (_, __) => this.DisplayState = DisplayState.Loading;
      TrackList.Instance.FinishedLoading += this._Instance_FinishedLoading;
      this._UpdateDisplayState();
    }

    //Device.BeginInvokeOnMainThread(() =>
    //todo: check why this still gets fired twice
    private void _Instance_FinishedLoading(object sender, EventArgs e) {
      this.Tracks = TrackList.Instance.ToList();
      this._UpdateDisplayState();
    }

    private void _UpdateDisplayState() {
      if (TrackList.Instance.IsLoading)
        this.DisplayState = DisplayState.Loading;
      else {
        this.DisplayState = this.Tracks.Count == 0
          ? DisplayState.Empty
          : DisplayState.DisplayingContent;
      }
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