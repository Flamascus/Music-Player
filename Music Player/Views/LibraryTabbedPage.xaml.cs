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
    private readonly MainLogic _logic = MainLogic.Instance;

    public LibraryTabbedPage() {
      Thread.Sleep(200); //todo: dunno why this is needed
      this.InitializeComponent();
      this._logic.TrackQueue.NewSongSelected += (args, sender) => ShowPlayer();
    }

    public void ShowPlayer() {
      if (this._firstSongPlayed)
        return;

      this.stackLayout.Children.Add(new BottomTrackView());
      this._firstSongPlayed = true;
    }

    private void _SearchClicked(object _, EventArgs __) => this.Navigation.PushAsync(new SearchPage());

    private void _ShuffleClicked(object sender, EventArgs e) {
      var queue = this._logic.TrackQueue;
      queue.ChangeQueue(this._logic.AllTracks);
      queue.Shuffle();
    }
  }
}