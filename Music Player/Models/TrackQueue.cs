using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music_Player.Models {
  public class TrackQueue { //todo: inherit from ienumerable

    public event EventHandler<TrackEventArgs> NewSongSelected;

    public List<ITrack> Tracks { get; private set; }

    //todo: make method instead of property
    public ITrack CurrentTrack {
      get => this._currentTrack;
      private set {
        this._currentTrack = value;
        this.NewSongSelected?.Invoke(this, new TrackEventArgs(value));
        _wasPaused = false;
      }
    }

    public int Index {
      get => this._index;
      set {
        this._index = value;       
        this.CurrentTrack = this.Tracks.Count > value ? this.Tracks[value] : null;
      }
    }

    //private Queue<ITrack> _tracks;
    private int _index;
    private ITrack _currentTrack;

    private bool _wasPaused; //indicates if the track was already paused or if its the first play   
    private IMediaManager _mediaManager;
    private static Random _rnd = new Random();
    public bool IsShuffle { get; private set; }

    public TrackQueue(List<ITrack> tracks) {
      var manager = CrossMediaManager.Current;
      this._mediaManager = manager;
      this.Tracks = tracks;

      manager.MediaItemFinished += _MediaItemFinished;
    }


    public void ChangeQueue(List<ITrack> tracks, int index = 0) {
      this.Tracks = tracks;
      this.Index = index;
    }

    private void _MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e) {
      this.Next();
    }


    private async void _PlayTrackAsync() {
      var manager = this._mediaManager;
      var track = this._currentTrack;
      var item = await manager.Extractor.CreateMediaItem(track.Path);
      await Task.Delay(50); //todo: is workaround, fix!

      await manager.Play(item);
    }


    //returns the enqueued track or null if none available
    //private ITrack _EnqueueNextTrack() {
    //  var nextIndex = this.Index + 1;
    //  if (this.Tracks.Count < nextIndex)
    //    return null;

    //  var track = this.Tracks[nextIndex];
    //  this._EnqueueTrackAsync(track);
    //  return track;
    //}


    //private async void _EnqueueTrackAsync(ITrack track) {
    //  var manager = this._mediaManager;
    //  var item = await manager.Extractor.CreateMediaItem(track.Path);
    //  await Task.Delay(50); //todo: is workaround, fix!
    //  manager.Queue.Add(item);
    //}

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
      ++this.Index;
      this._PlayTrackAsync();
    }

    //todo: implement again
    public void Previous() {
      --this.Index;
      this._PlayTrackAsync();
    }

    public void Shuffle() {
      _ShuffleList(this.Tracks);
      this.Index = 0;
      this._PlayTrackAsync();
      this.IsShuffle = true;
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

  }
}
