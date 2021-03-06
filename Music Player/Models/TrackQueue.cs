using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models {
  public class TrackQueue { //todo: inherit from ienumerable

    public static TrackQueue Instance = new TrackQueue();

    public event EventHandler<TrackEventArgs> NewSongSelected;

    public List<ITrack> NextUpTracks { get; private set; } = new List<ITrack>();
    public List<ITrack> QueuedTracks { get; private set; } = new List<ITrack>();
    public List<ITrack> TrackHistory { get; private set; } = new List<ITrack>(); //todo: implement properly!


    //todo: make method instead of property
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

    //private int _index;
    private ITrack _currentTrack;

    private bool _wasPaused; //indicates if the track was already paused or if its the first play   
    private IMediaManager _mediaManager;
    private static Random _rnd = new Random();
    public bool IsShuffle { get; private set; }

    private TrackQueue() {
      var manager = CrossMediaManager.Current;
      this._mediaManager = manager;
      manager.MediaItemFinished += _MediaItemFinished;
    }

    public void ChangeQueue(List<ITrack> tracks) {
      this.QueuedTracks = tracks;

      this.CurrentTrack = _Dequeue(tracks);
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
        this.Play(_Dequeue(this.NextUpTracks));

      else if (this.QueuedTracks.Count > 0)       
          this.Play(_Dequeue(this.QueuedTracks));
    }

    //todo: implement again
    public void Previous() {
      //--this.Index;
      //this._PlayTrackAsync();
    }

    public void Shuffle() {
      _ShuffleList(this.QueuedTracks);
      var track = this.QueuedTracks[0];
      this.QueuedTracks.RemoveAt(0);
      this.CurrentTrack = track;
      this.IsShuffle = true;
      this.Play();
    }

    //todo: put into track and not queue!
    public void JumpToPercent(double value) {
      var duration = this._currentTrack.Duration;
      var position = TimeSpan.FromTicks((long)(duration.Ticks * value));
      this._mediaManager.SeekTo(position);
    }

    //todo: put into track and not queue!
    public double GetProgress() {
      var duration = this._currentTrack.Duration;
      var position = this._mediaManager.Position;
      return ((double)position.Ticks / duration.Ticks);
    }

    private static void _ShuffleList<T>(List<T> list) {
      var i = list.Count;

      while (i > 1) {
        i--; //todo: can probably put this into line above
        var k = _rnd.Next(i + 1);
        var temp = list[k];
        list[k] = list[i];
        list[i] = temp;
      }
    }

    //todo: extension method
    private static T _Dequeue<T>(List<T> list) {
      var item = list.FirstOrDefault();
      list.RemoveAt(0);
      return item;
    }

  }
}
