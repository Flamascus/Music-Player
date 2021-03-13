using Music_Player.Droid.Classes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SmallTrackView : ContentView {

    public class OptionsEventArgs : EventArgs {
      public Track Track { get; }

      public OptionsEventArgs(Track track) : base() {
        this.Track = track;
      }
    }

    public static readonly BindableProperty TrackProperty
      = BindableProperty.Create(nameof(ImageSource), typeof(Track), typeof(SmallTrackView), null, propertyChanged: _OnTrackChanged);

    public event EventHandler<EventArgs> Tapped;
    public event EventHandler<EventArgs> OptionsTapped;  

    public Track Track {
      get => (Track)this.GetValue(TrackProperty);
      set => this.SetValue(TrackProperty, value);
    }

    private Track _track; //todo: can be deleted probably

    private static void _OnTrackChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (SmallTrackView)bindable;
      var track = (Track)newValue;

      if (track == null)
        return;

      control._track = track;
      control.lblTitle.Text = track.Title;
      control.lblArtists.Text = track.ArtistString;
      control.imgCover.Source = track.CoverSource;
    }

    public SmallTrackView() {
      this.InitializeComponent();
    }

    private void _ControlTapped(object _, EventArgs e) => this.Tapped?.Invoke(this, e);
    private void _OptionsTapped(object _, EventArgs e) => this.OptionsTapped?.Invoke(this, new OptionsEventArgs(this._track));

    
  }
}