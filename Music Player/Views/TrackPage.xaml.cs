using System;
using Music_Player.Interfaces;
using Music_Player.Models;
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
      var model = TrackViewModel.Instance;
      this._model = model;
      this.BindingContext = model;
      //this.Cover.MinimumHeightRequest = this.Cover.Width;

      TrackQueue.Instance.NewSongSelected += OnNewSongSelected;

      var timer = new System.Threading.Timer(this._UpdateSlider, null, 0, 500);
    }

    protected override void OnAppearing() => this._SetBarColors();

    protected override void OnDisappearing() {
      TrackQueue.Instance.NewSongSelected -= OnNewSongSelected;
      _nativeFeatures.SetStatusBarColor(Color.FromRgb(0, 79, 163));
      _nativeFeatures.SetNavigationBarColor(Color.Black);
    }

    private void OnNewSongSelected(object sender, TrackEventArgs e) => this._SetBarColors();

    private void _SetBarColors() {
      _nativeFeatures.SetStatusBarColor(this.GradientStart.Color);
      _nativeFeatures.SetNavigationBarColor(this.GradientEnd.Color);
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