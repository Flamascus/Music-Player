using System;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;
using System.Threading.Tasks;
using Music_Player.Models;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    public SongsViewModel ViewModel { get; set; }

    public SongsView() {
      var model = new SongsViewModel();
      this.ViewModel = model;
      this.BindingContext = model;

      this.InitializeComponent();

      model.StartedLoading += this._Model_StartedLoading;
      model.FinishedLoading += this._Model_FinishedLoading;

      this._UpdateDisplayState();
    }

    private void _Model_StartedLoading(object sender, EventArgs e)
      => Device.BeginInvokeOnMainThread(() => this._UpdateDisplayState());

    private void _Model_FinishedLoading(object sender, EventArgs e)
      => Device.BeginInvokeOnMainThread(() => this._UpdateDisplayState());

    /// <summary>
    /// switches between showing items, or loading or no displayed items
    /// </summary>
    private void _UpdateDisplayState() {
      var isLoading = this.ViewModel.IsLoading;
      this._ShowLoading(isLoading);

      if (!isLoading && this.ViewModel.Tracks.Count == 0) {
        this.lvTracks.IsVisible = false;
        this.lblNoTracks.IsVisible = true;
      }
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