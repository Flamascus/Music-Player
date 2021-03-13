using Music_Player.Droid.Classes;
using Music_Player.Models;
using Music_Player.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
  public class TrackViewModel : ANotifyPropertyChanged {   

    public Track Track {
      get => this._track;
      set {
        this._track = value;
        this.OnPropertyChanged(nameof(this.Title));
        this.OnPropertyChanged(nameof(this.Producer));
        this.OnPropertyChanged(nameof(this.CoverSource));
        this.OnPropertyChanged(nameof(this.PlayPauseImageSource));
      }
    }

    //todo: only temp!!
    public ICollection<Track> Tracks => TrackQueue.Instance.AllTracks;

    public string Title => this.Track.Title;
    public string Producer => this.Track.ArtistString;
    public ImageSource CoverSource => this.Track.Cover.Source;
    public string PlayPauseImageSource => this._isPlaying ? "pause.png" : "play.png";
    public string ShuffleImageSource => this._queue.IsShuffle ? "shuffle_selected.png" : "shuffle.png";

    //colors used for gradient of trackview
    public Color Color { get; private set; }
    public Color ColorDark { get; private set; }

    private bool _isPlaying;

    private readonly TrackQueue _queue; 
    private Track _track;

    public static TrackViewModel Instance = _instance ??= new TrackViewModel();
    private static TrackViewModel _instance;

    private TrackViewModel() {
      var queue = TrackQueue.Instance;

      this._queue = queue;
      this.Track = queue.CurrentTrack;
      
      this.PlayTapCommand = new Command(this.PlayTapped);
      this.NextTapCommand = new Command(this.NextTapped);
      this.PreviousTapCommand = new Command(this.PreviousTapped);
      this.ShuffleTapCommand = new Command(this.ShuffleTapped);
      queue.NewSongSelected += this._OnNewSongSelected;

      this._GetColors();
    }

    private void _GetColors() {
      var color = this._track.Cover.GetDominantColor();

      if (color.R == 0 && color.G == 0 && color.B == 0) {
        this.Color = Color.DimGray;
        this.ColorDark = color;
      } else {
        this.Color = color;
        this.ColorDark = new Color(color.R / 3, color.G / 3, color.B / 3, color.A);
      }

      this.OnPropertyChanged(nameof(this.Color));
      this.OnPropertyChanged(nameof(this.ColorDark));
    }

    public ICommand PlayTapCommand { get; }
    public ICommand NextTapCommand { get; }
    public ICommand PreviousTapCommand { get; }
    public ICommand ShuffleTapCommand { get; }

    public void PlayTapped() {
      this._isPlaying = !this._isPlaying;
      this.OnPropertyChanged(nameof(this.PlayPauseImageSource));

      if (this._isPlaying)
        this._queue.Play();
      else
        this._queue.Pause();
    }

    public void NextTapped() => this._queue.Next();
    public void PreviousTapped() => this._queue.Previous();
    public void ShuffleTapped() {
      this._queue.Shuffle();
      this.OnPropertyChanged(nameof(this.ShuffleImageSource));
    }

    private void _OnNewSongSelected(object sender, TrackEventArgs args) {
      this.Track = args.Track;
      this._isPlaying = true;
      this._GetColors();
    }
    
    public void TrackPositionChanged(double value) => this._queue.CurrentTrack.JumpToPercent(value);

    public double Progress => this._queue.CurrentTrack.GetProgressPercent();
  }
}
