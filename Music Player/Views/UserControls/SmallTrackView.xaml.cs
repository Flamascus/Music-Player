using Music_Player.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SmallTrackView : ContentView {

    public class OptionsEventArgs : EventArgs {
      public ITrack Track { get; }

      public OptionsEventArgs(ITrack track) : base() {
        this.Track = track;
      }
    }

    public static readonly BindableProperty TrackProperty
      = BindableProperty.Create(nameof(ImageSource), typeof(ITrack), typeof(SmallTrackView), null, propertyChanged: _OnTrackChanged);

    public event EventHandler<EventArgs> OptionsTapped; 

    public ITrack Track {
      get => (ITrack)GetValue(TrackProperty);
      set => SetValue(TrackProperty, value);
    }

    private ITrack _track;

    private static void _OnTrackChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (SmallTrackView)bindable;
      var track = (ITrack)newValue;

      control._track = track;
      control.lblTitle.Text = track.Title;
      control.lblArtists.Text = track.CombinedArtistNames;
      control.imgCover.Source = track.CoverSource;
    }

    public SmallTrackView() {
      this.InitializeComponent();
    }

    private void _OptionsTapped(object sender, EventArgs e) => this.OptionsTapped?.Invoke(sender, new OptionsEventArgs(this._track));
  }
}