using Music_Player.Models;
using Music_Player.Services;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class FolderView : ContentView {
    public FolderView() {
      this.InitializeComponent();
    }

    //todo: find better way of doing this
    private void _FolderTapped(object sender, EventArgs e) {
      var grid = (Grid)sender;
      var label = (Label)grid.Children.Last();
      var dirName = label.Text;
      this.ViewModel.OnFolderTapped(dirName);
    }

    private async void _TrackOptionsTapped(object sender, TrackEventArgs e) => await TrackOptions.DisplayBasicOptionsAsync(e.Track);

    private void _TrackTapped(object sender, EventArgs e) {
      var trackView = (SmallTrackView)sender;
      this.ViewModel.OnTrackTapped(trackView.Track);
    }

  }
}