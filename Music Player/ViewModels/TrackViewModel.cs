using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
  public class TrackViewModel : INotifyPropertyChanged {

    public ITrack Track {
      get => this._track;
      set {
        this._track = value;
        this._isPlaying = true;
        this.OnPropertyChanged(nameof(Title));
        this.OnPropertyChanged(nameof(Producer));
        this.OnPropertyChanged(nameof(CoverSource));
        this.OnPropertyChanged(nameof(PlayPauseImageSource));
      }
    }

    public string Title => this.Track.Title;
    public string Producer => this.Track.Producer;
    public ImageSource CoverSource => this.Track.CoverSource;
    public string PlayPauseImageSource => this._isPlaying ? "pause.png" : "play.png";

    //colors used for gradient of trackview
    public Color Color { get; }
    public Color ColorDark { get; }

    private bool _isPlaying;

    private MainLogic _logic;
    private ITrack _track;

    public TrackViewModel() {
      var logic = MainLogic.Instance;
      this._logic = logic;
      this._track = logic.CurrentTrack; //todo: shouldnt access backing property
      this.PlayTapCommand = new Command(OnPlayTapped);

      var color =  this._track.GetImageColor();

      if (color.R == 0 && color.G == 0 && color.B == 0) {
        this.Color = Color.DimGray;
        this.ColorDark = color;
      } else {
        this.Color = color;
        this.ColorDark = new Color(color.R / 2, color.G / 2, color.B / 2, color.A);
      }
    }

    public ICommand PlayTapCommand { get; }

    public void OnPlayTapped() {
      this._isPlaying = !this._isPlaying;
      this.OnPropertyChanged(nameof(PlayPauseImageSource));

      if (this._isPlaying)
        this._logic.Play();
      else
        this._logic.Pause();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

  }
}
