using Music_Player.Services;
using Music_Player.Views.UserControls;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LibraryTabbedPage : ContentPage {

    private readonly MainLogic _logic = MainLogic.Instance;

    public LibraryTabbedPage() {
      Thread.Sleep(200); //todo: dunno why this is needed
      this.InitializeComponent();

      this._logic.TrackQueue.NewSongSelected += ShowPlayer;
    }

    public void ShowPlayer(object _, TrackEventArgs __) {
      this.stackLayout.Children.Add(new BottomTrackView());
      this._logic.TrackQueue.NewSongSelected -= ShowPlayer;
    }

    private void _SearchClicked(object _, EventArgs __) => this.Navigation.PushAsync(new SearchPage());

    private void _ShuffleClicked(object sender, EventArgs e) {
      var queue = this._logic.TrackQueue;
      queue.ChangeQueue(this._logic.AllTracks);
      queue.Shuffle();
    }
  }
}