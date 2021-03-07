using Music_Player.Models;
using Music_Player.Services;
using Music_Player.Views.UserControls;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LibraryTabbedPage : ContentPage {

    public LibraryTabbedPage() {
      this.InitializeComponent();

      TrackQueue.Instance.NewSongSelected += this.ShowPlayer;
    }

    public void ShowPlayer(object _, TrackEventArgs __) {
      this.stackLayout.Children.Add(new BottomTrackView());
      TrackQueue.Instance.NewSongSelected -= this.ShowPlayer;
    }

    private void _SearchClicked(object _, EventArgs __) => this.Navigation.PushAsync(new SearchPage());

    private void _ShuffleClicked(object sender, EventArgs e) {
      var queue = TrackQueue.Instance;
      queue.ChangeQueue(TrackList.Instance.ToList());
      queue.Shuffle();
    }
  }
}