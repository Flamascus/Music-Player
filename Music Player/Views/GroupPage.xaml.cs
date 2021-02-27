using Music_Player.Interfaces;
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
    private readonly MainLogic _logic = MainLogic.Instance;

    //todo: dont give contentview, just give tracklist lol
    public GroupPage(List<ITrack> tracks, string title) {
      this._tracks = tracks;
      this.Title = title;
      this.InitializeComponent();
      //NavigationPage.SetTitleView(this, new BottomTrackView());
      this.Grid.Children.Add(new SongsView(tracks), 0, 0);

      var queue = this._logic.TrackQueue;

      if (queue.CurrentTrack != null)
        this.ShowPlayer(null, null);
      else
        queue.NewSongSelected += ShowPlayer;
    }

    public void ShowPlayer(object _, TrackEventArgs __) {
      this.Grid.Children.Add(new BottomTrackView(), 0, 1);
      this._logic.TrackQueue.NewSongSelected -= ShowPlayer;
    }

    private void _ShuffleClicked(object _, EventArgs __) {
      var queue = MainLogic.Instance.TrackQueue;
      queue.ChangeQueue(this._tracks);
      queue.Shuffle();
    }
  }
}