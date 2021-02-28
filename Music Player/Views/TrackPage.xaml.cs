using System;
using Music_Player.Interfaces;
using Music_Player.Services;
using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TrackPage : ContentPage {

    private static INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();
    private readonly TrackViewModel _model;

    public TrackPage() {
      this.InitializeComponent();
      var model = MainLogic.Instance.TrackViewModel;
      this._model = model;
      this.BindingContext = model;
      this.Gradient.StartColor = model.Color;
      this.Gradient.EndColor = model.ColorDark;
      this.Cover.MinimumHeightRequest = this.Cover.Width;

      var timer = new System.Threading.Timer(this._UpdateSlider, null, 0, 500);
    }

    protected override void OnAppearing() {
      _nativeFeatures.SetStatusBarColor(this.Gradient.StartColor);
      _nativeFeatures.SetNavigationBarColor(this.Gradient.EndColor);
    }

    protected override void OnDisappearing() {
      _nativeFeatures.SetStatusBarColor(Color.FromRgb(0, 79, 163));
      _nativeFeatures.SetNavigationBarColor(Color.Black);
    }

    private void _PlaylistTapped(object sender, EventArgs e) {
      this.Navigation.PushModalAsync(new QueuePage());
    }

    private void _CloseTapped(object sender, EventArgs e) {
      this.Navigation.PopModalAsync();
    }

    private void _UpdateSlider(object _) => this.Slider.Value = this._model.Progress;
    private void Slider_DragCompleted(object _, EventArgs __) => this._model.TrackPositionChanged(this.Slider.Value);
  }
}