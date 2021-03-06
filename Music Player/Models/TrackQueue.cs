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
    public List<ITrack> AllTracks { get; private set; } = new List<ITrack>();

    public ITrack CurrentTrack {
      get => this._currentTrack;
      private set {
        if (this._currentTrack != null)
          this.TrackHistory.Add(this._currentTrack);

        this._currentTrack = value;
        this.NewSongSelected?.Invoke(this, new TrackEventArgs(value));
        this._wasPaused = false;
      }
    }

    public int Index { get; private set; } //todo: make index setting public?

    private ITrack _currentTrack;

    private bool _wasPaused; //indicates if the track was already paused or if its the first play   
    private readonly IMediaManager _mediaManager;    
    public bool IsShuffle { get; private set; }

    private TrackQueue() {
      var manager = CrossMediaManager.Current;
      this._mediaManager = manager;
      manager.MediaItemFinished += this._MediaItemFinished;
    }

    public void ChangeQueue(List<ITrack> tracks) {
      this.QueuedTracks = tracks;
      this.CurrentTrack = tracks.Dequeue();
      this._ReloadFullQueue();
    }

    public void AddNext(ITrack track) {
      this.NextUpTracks.Insert(0, track);
      this._ReloadFullQueue();
    }

    public void AddToQueue(ITrack track) {
      this.NextUpTracks.Add(track);
      this._ReloadFullQueue();
    }

    public void JumpToNextUpTrack(ITrack track) => this._JumpToClickedTrack(track, this.NextUpTracks);

    public void JumpToQueueTrack(ITrack track) => this._JumpToClickedTrack(track, this.QueuedTracks);

    private void _JumpToClickedTrack(ITrack track, List<ITrack> tracks) {
      for (var i = 0; i < tracks.Count; ++i) {
        if (tracks[i] == track) {
          tracks.RemoveRange(0, i + 1);
          this.Play(track);
          break;
        }
      }

      this._ReloadFullQueue();
    }

    private void _ReloadFullQueue() {
      var tracks = new List<ITrack>(this.TrackHistory) { this.CurrentTrack };
      tracks.AddRange(this.NextUpTracks);
      tracks.AddRange(this.QueuedTracks);
      this.AllTracks = tracks;

      this.Index = this.TrackHistory.Count + 1;
    }

    private void _MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e) => this.Next();

    public void Play(ITrack track) {
      this.CurrentTrack = track;
      this._mediaManager.Play(track.Path);
    }

    public void Play() {
      if (!this._wasPaused) {
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

      ++this.Index;
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
      this._ReloadFullQueue();
      this.Play();
    }

  }
}
