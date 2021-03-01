using System;
using Music_Player.Interfaces;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    private readonly SongsViewModel _model = new SongsViewModel();

    public SongsView() {
      var model = new SongsViewModel();
      this._model = model;
      this.BindingContext = model;
      this.InitializeComponent();
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