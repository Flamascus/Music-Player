using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  class SongsViewModel : ANotifyPropertyChanged, ILoadable {

    private List<Track> _tracks;

    public event EventHandler<EventArgs> FinishedLoading;

    public List<Track> Tracks {
      get => this._tracks;
      set {
        this.OnPropertyChanged();
        this._tracks = value;
      }
    }

    public bool IsLoading {
      get => this._isLoading;
      protected set {
        this._isLoading = value;
        if (!value)
          this.FinishedLoading?.Invoke(this, new EventArgs());
      }
    }

    private bool _isLoading;

  public SongsViewModel() {
      this.Tracks = TrackList.Instance.ToList();

      if (TrackList.Instance.IsLoading) {
        this.IsLoading = true;
        TrackList.Instance.FinishedLoading += this._Instance_FinishedLoading;
      }
    }

    //todo: check why this gets fired twice
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