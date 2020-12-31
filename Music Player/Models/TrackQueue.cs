using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Player.Models {
  public class TrackQueue {

    public event EventHandler<TrackEventArgs> NewSongSelected;

    public Queue<ITrack> Tracks {
      get => _tracks;
      set {
        _tracks = value;

        if (value.Count > 1)
          this.CurrentTrack = value.Dequeue();

        this._EnqueueTrack();
      }
    }

    //todo: make method instead of property
    public ITrack CurrentTrack {
      get => this._currentTrack;
      private set {
        this._currentTrack = value;
        this.NewSongSelected?.Invoke(this, new TrackEventArgs(value));
        _wasPaused = false;
      }
    }

    private Queue<ITrack> _tracks;
    private ITrack _currentTrack;

    private bool _wasPaused; //indicates if the track was already paused or if its the first play   
    private IMediaManager _mediaManager;
    private static Random _rnd = new Random();
    private MainLogic _logic = MainLogic.Instance;

    public TrackQueue(List<ITrack> tracks) {
      var manager = CrossMediaManager.Current;
      this._mediaManager = manager;
      this.Tracks = new Queue<ITrack>(tracks);

      manager.MediaItemFinished += _MediaItemFinished;
      manager.MediaItemChanged += _MediaItemFinished;
      manager.MediaItemChanged += _MediaItemChanged;
    }

    private void _MediaItemChanged(object sender, MediaManager.Media.MediaItemEventArgs e) { //todo: probably gets called to often
      this._EnqueueTrack();

      this.NewSongSelected?.Invoke(this, new TrackEventArgs(
        this._logic.AllTracks.Where(t => t.Path == this._mediaManager.Queue.Current.MediaUri).FirstOrDefault()
        ));
    }

    private void _MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e) {

    }

    //returns the enqueued track or null if none available
    private ITrack _EnqueueTrack() {
      if (this.Tracks.Count == 0)
        return null;

      var track = this.Tracks.Dequeue();
      this._EnqueueTrackAsync(track);
      return track;
    }

    private async void _EnqueueTrackAsync(ITrack track) {
      var manager = this._mediaManager;
      var item = await manager.Extractor.CreateMediaItem(track.Path);
      await Task.Delay(50); //todo: is workaround, fix!
      manager.Queue.Add(item);
    }

    public void Play() {
      if (!_wasPaused) {
        this._mediaManager.Play(this.CurrentTrack.Path);
        //var next = this._mediaManager.Extractor.CreateMediaItem(this.AllTracks.Last().Path);
        //next.Wait();
        //this._mediaManager.Queue.Add(next.Result);
      } else
        this._mediaManager.Play();
    }

    public void Pause() {
      this._mediaManager.Pause();
      this._wasPaused = true;
    }

    public void Next() {
      this._EnqueueTrack();
      this._mediaManager.PlayNext();
    }

    public void Previous() {
      this._mediaManager.PlayPrevious();
    }

    public void Shuffle() {
      var queue = this.Tracks;
      var list = queue.ToList();
      _ShuffleList(list);
      this.Tracks = new Queue<ITrack>(list);
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
