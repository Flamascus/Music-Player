using Music_Player.Services;
using Music_Player.Views.UserControls;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class PlaylistPage : ContentPage {
    public PlaylistPage() {
      this.InitializeComponent();
      var children = this.stackLayout.Children;

      //todo: should be done in xaml
      children.Add(new Label { Text = "Next Up:" });
      children.Add(new SongsView(MainLogic.Instance.TrackQueue.Tracks.ToList()));
    }
  }
}