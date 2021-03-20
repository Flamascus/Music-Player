using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;
using System.Threading.Tasks;
using Music_Player.Models;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    public Task DisplayActionSheet { get; private set; }
    public List<Track> Tracks {
      get => this.ViewModel.Tracks;
      set => this.ViewModel.Tracks = value;
    }

    public SongsView() {
      this.InitializeComponent();
    }

    private void _TrackView_Tapped(object sender, EventArgs e) {
      var trackView = (SmallTrackView)sender;
      this.ViewModel.OnTrackTapped(trackView.Track);
    }

    private async void _OptionsTapped(object _, OptionsEventArgs e) => await TrackOptions.DisplayBasicOptionsAsync(e.Track);
  }
}