using Music_Player.Services;
using Music_Player.Views.UserControls;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LibraryTabbedPage : ContentPage {

    private bool _firstSongPlayed = false;

    public LibraryTabbedPage() {
      Thread.Sleep(200); //todo: dunno why this is needed
      this.InitializeComponent();
      MainLogic.Instance.TrackQueue.NewSongSelected += (args, sender) => ShowPlayer();
    }

    public void ShowPlayer() {
      if (this._firstSongPlayed)
        return;

      this.stackLayout.Children.Add(new BottomTrackView());
      this._firstSongPlayed = true;
    }

    private void _SearchClicked(object _, EventArgs __) => this.Navigation.PushAsync(new SearchPage());
  }
}