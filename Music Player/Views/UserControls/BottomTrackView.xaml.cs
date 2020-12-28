using Music_Player.Services;
using Music_Player.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BottomTrackView : ContentView {

    public TrackViewModel Model { get; }

    public BottomTrackView() {
      this.InitializeComponent();
      var model = new TrackViewModel();
      this.Model = model;
      this.BindingContext = model;
      MainLogic.Instance.NewSongSelected += _OnNewSongSelected;     
    }

    public void OnPlayTapped(object sender, EventArgs args) => this.Model.OnPlayTapped(); //todo: use icommand instead

    public void OnViewTapped(object sender, EventArgs args) {
      this.Navigation.PushModalAsync(new TrackPage());
    }

    private void _OnNewSongSelected(object sender, TrackEventArgs args) {
      this.Model.Track = args.Track;
    }

  }
}