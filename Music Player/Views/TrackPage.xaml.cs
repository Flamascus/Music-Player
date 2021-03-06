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

    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();
    private readonly TrackViewModel _model;

    private bool isSwipe;

    public TrackPage() {
      this.InitializeComponent();
      var model = TrackViewModel.Instance;
      this._model = model;
      this.BindingContext = model;
      this._SetCarouselIndex();

      TrackQueue.Instance.NewSongSelected += this._OnNewSongSelected;
      var timer = new System.Threading.Timer(this._UpdateSlider, null, 0, 500);
    }

    private void _Carousel_PositionChanged(object sender, PositionChangedEventArgs e) {
      this.isSwipe = true;

      if (e.CurrentPosition == e.PreviousPosition + 1)
        TrackQueue.Instance.Next();
      else if (e.CurrentPosition == e.PreviousPosition - 1)
        TrackQueue.Instance.Previous();

      this.isSwipe = false;
    }

    protected override void OnAppearing() => this._SetBarColors();

    protected override void OnDisappearing() {     
      _nativeFeatures.SetStatusBarColor(Color.FromRgb(0, 79, 163));
      _nativeFeatures.SetNavigationBarColor(Color.Black);
    }

    private void _OnNewSongSelected(object _, TrackEventArgs __) {
      if (!this.isSwipe)
        this._SetCarouselIndex();

        this._SetBarColors();
    }

    private void _SetCarouselIndex() {      
      this.carousel.PositionChanged -= this._Carousel_PositionChanged;
      this.carousel.Position = TrackQueue.Instance.Index - 1;
      this.carousel.PositionChanged += this._Carousel_PositionChanged;
    }


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
    private void _Slider_DragCompleted(object _, EventArgs __) => this._model.TrackPositionChanged(this.Slider.Value);
  }
}