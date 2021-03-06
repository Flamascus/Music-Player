using Music_Player.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BottomTrackView : ContentView {

    private readonly TrackViewModel _model;

    public BottomTrackView() {
      this.InitializeComponent();
      this._model = TrackViewModel.Instance;
      this.BindingContext = this._model;
      var timer = new System.Threading.Timer(this._UpdateSlider, null, 0, 500);
    }

    public void OnViewTapped(object sender, EventArgs args) {
      this.Navigation.PushModalAsync(new TrackPage());
    }

    private void _UpdateSlider(object _) => this.Slider.Value = this._model.Progress;
    private void Slider_DragCompleted(object _, EventArgs __) => this._model.TrackPositionChanged(this.Slider.Value);
  }
}