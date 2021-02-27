using Music_Player.Models;
using Music_Player.Services;
using Music_Player.Views.UserControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GenrePage : ContentPage {

    private readonly Genre _genre;

    //todo: dont give contentview, just give tracklist lol
    public GenrePage(Genre genre) {
      this._genre = genre;
      this.Title = genre.GenreName;
      this.InitializeComponent();
      //NavigationPage.SetTitleView(this, new BottomTrackView());
      this.Grid.Children.Add(new SongsView(genre.Tracks), 0, 0);
    }

    private void _ShuffleClicked(object sender, EventArgs e) {
      var queue = MainLogic.Instance.TrackQueue;
      queue.ChangeQueue(this._genre.Tracks);
      queue.Shuffle();
    }
  }
}