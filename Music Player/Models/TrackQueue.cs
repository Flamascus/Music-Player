using MediaManager;
using Music_Player.Helpers;
using Music_Player.Interfaces;
using Music_Player.Services;
using System;
using System.Collections.Generic;

namespace Music_Player.Models {
  public class TrackQueue { //todo: inherit from ienumerable

    public static TrackQueue Instance = new TrackQueue();

    public event EventHandler<TrackEventArgs> NewSongSelected;

    public List<ITrack> NextUpTracks { get; private set; } = new List<ITrack>();
    public List<ITrack> QueuedTracks { get; private set; } = new List<ITrack>();
    public List<ITrack> TrackHistory { get; private set; } = new List<ITrack>(); //todo: implement properly!

    public List<ITrack> AllTracks { //todo: implement properly!
      get {
        var tracks = new List<ITrack>(NextUpTracks);
        tracks.AddRange(QueuedTracks);
        return tracks;
      }
    }

    public ITrack CurrentTrack {
      get => this._currentTrack;
      set {
        if (this._currentTrack != null)
          this.TrackHistory.Add(this._currentTrack);

        this._currentTrack = value;
        this.NewSongSelected?.Invoke(this, new TrackEventArgs(value));
        _wasPaused = false;
      }
    }

    private ITrack _currentTrack;

    private bool _wasPaused; //indicates if the track was already paused or if its the first play   
    private IMediaManager _mediaManager;    
    public bool IsShuffle { get; private set; }

    private TrackQueue() {
      var manager = CrossMediaManager.Current;
      this._mediaManager = manager;
      manager.MediaItemFinished += _MediaItemFinished;
    }

    public void ChangeQueue(List<ITrack> tracks) {
      this.QueuedTracks = tracks;

      this.CurrentTrack = tracks.Dequeue();
    }

    private void _MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e) {
      this.Next();
    }

    public void Play(ITrack track) {
      this.CurrentTrack = track;
      this._mediaManager.Play(track.Path);
    }

    public void Play() {
      if (!_wasPaused) {
        this._mediaManager.Play(this.CurrentTrack.Path);
      } else
        this._mediaManager.Play();
    }

    public void Pause() {
      this._mediaManager.Pause();
      this._wasPaused = true;
    }

    public void Next() {
      if (this.NextUpTracks.Count > 0)
        this.Play(this.NextUpTracks.Dequeue());

      else if (this.QueuedTracks.Count > 0)
        this.Play(this.QueuedTracks.Dequeue());
    }

    //todo: implement again
    public void Previous() {
      //--this.Index;
      //this._PlayTrackAsync();
    }

    public void Shuffle() {
      this.QueuedTracks.Shuffle();
      var track = this.QueuedTracks[0];
      this.QueuedTracks.RemoveAt(0);
      this.CurrentTrack = track;
      this.IsShuffle = true;
      this.Play();
    }

  }
}
