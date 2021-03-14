using System;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;
using System.Threading.Tasks;
using Music_Player.Models;
using Music_Player.Droid.Classes;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    public SongsView() {
      this.InitializeComponent();
      var model = this.ViewModel;

      if (model.IsLoading) { //todo: put loading stuff in extra class and inherit from that
        this._ShowLoading(true);
        model.FinishedLoading += this._Model_FinishedLoading;

      } else if (model.Tracks.Count == 0) { //todo: can probably write this more beautiful
        this.lvTracks.IsVisible = false;
        this.lblNoTracks.IsVisible = true;
      }
    }

    private void _Model_FinishedLoading(object sender, EventArgs e) {
      Device.BeginInvokeOnMainThread(() => {
        this._ShowLoading(false);

        if (this.ViewModel.Tracks.Count == 0) { //todo: can probably write this more beautiful
          this.lvTracks.IsVisible = false;
          this.lblNoTracks.IsVisible = true;
        }

        this.ViewModel.FinishedLoading -= this._Model_FinishedLoading;
      });
    }

    private void _ShowLoading(bool isLoading = true) {
      this.activityIndicator.IsRunning = isLoading;
      this.activityIndicator.IsVisible = isLoading;
      this.lvTracks.IsVisible = !isLoading;
    }

    public SongsView(List<Track> tracks) {
      var model = new SongsViewModel(tracks);
      this.ViewModel = model; //todo: probably only need to set either viewmodel or bindingcontext, not both
      this.BindingContext = model;
      this.InitializeComponent();
    }

    public List<Track> Tracks { get; }
    public Task DisplayActionSheet { get; private set; }

    private void _TrackView_Tapped(object sender, EventArgs e) {
      var trackView = (SmallTrackView)sender;
      this.ViewModel.OnTrackTapped(trackView.Track);
    }

    private async void _OptionsTapped(object _, OptionsEventArgs e) => await TrackOptions.DisplayBasicOptionsAsync(e.Track);
  }
}