using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using Music_Player.Views.UserControls;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GroupPage : ContentPage {

    private readonly List<ITrack> _tracks;

    //todo: dont give contentview, just give tracklist lol
    public GroupPage(List<ITrack> tracks, string title) {
      this._tracks = tracks;
      this.Title = title;
      this.InitializeComponent();
      //NavigationPage.SetTitleView(this, new BottomTrackView());
      this.Grid.Children.Add(new SongsView(tracks), 0, 0);

      var queue = TrackQueue.Instance;

      if (queue.CurrentTrack != null)
        this.ShowPlayer(null, null);
      else
        queue.NewSongSelected += ShowPlayer;
    }

    public void ShowPlayer(object _, TrackEventArgs __) {
      this.Grid.Children.Add(new BottomTrackView(), 0, 1);
      TrackQueue.Instance.NewSongSelected -= ShowPlayer;
    }

    private void _ShuffleClicked(object _, EventArgs __) {
      var queue = TrackQueue.Instance;
      queue.ChangeQueue(this._tracks);
      queue.Shuffle();
    }
  }
}