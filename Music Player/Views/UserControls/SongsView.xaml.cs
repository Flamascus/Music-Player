using System;
using Music_Player.Interfaces;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;
using System.Diagnostics;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    private readonly SongsViewModel _model = new SongsViewModel();

    public SongsView() {
      var model = new SongsViewModel();
      this._model = model;
      this.BindingContext = model;

      this.InitializeComponent();

      if (model.IsLoading) { //todo: put loading stuff in extra class and inherit from that
        this._ShowLoading(true);
        model.FinishedLoading += this._Model_FinishedLoading;

      } else if (this._model.Tracks.Count == 0) { //todo: can probably write this more beautiful
        this.lvTracks.IsVisible = false;
        this.lblNoTracks.IsVisible = true;
      }
    }

    private void _Model_FinishedLoading(object sender, EventArgs e) {
      Device.BeginInvokeOnMainThread(() => {
        this._ShowLoading(false);

        if (this._model.Tracks.Count == 0) { //todo: can probably write this more beautiful
          this.lvTracks.IsVisible = false;
          this.lblNoTracks.IsVisible = true;
        }

        this._model.FinishedLoading -= this._Model_FinishedLoading;
      });
    }

    private void _ShowLoading(bool isLoading = true) {
      this.activityIndicator.IsRunning = isLoading;
      this.activityIndicator.IsVisible = isLoading;
      this.lvTracks.IsVisible = !isLoading;
    }

    public SongsView(List<ITrack> tracks) {
      var model = new SongsViewModel(tracks);
      this._model = model;
      this.BindingContext = model;
      this.InitializeComponent();
    }

    public List<ITrack> Tracks { get; }

    private void TrackView_Tapped(object sender, EventArgs e) {
      var trackView = (SmallTrackView)sender;
      this._model.OnTrackTapped(trackView.Track);
    }

    private void _OptionsTapped(object sender, OptionsEventArgs e) => this.Navigation.PushAsync(new OptionsPage(e.Track));
  }
}