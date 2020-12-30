using Music_Player.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BottomTrackView : ContentView {

    public BottomTrackView() {
      this.InitializeComponent();
      this.BindingContext = MainLogic.Instance.TrackViewModel;
    }

    public void OnViewTapped(object sender, EventArgs args) {
      this.Navigation.PushModalAsync(new TrackPage());
    }

  }
}