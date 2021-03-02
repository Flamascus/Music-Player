using System;
using Music_Player.Services;
using Music_Player.ViewModels;
using Music_Player.Views.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class QueuePage : ContentPage {

    private readonly QueueViewModel _model;

    public QueuePage() {
      this.InitializeComponent();
      this._model = new QueueViewModel();
      this.BindingContext = _model;
    }

    private void NextUpTrackView_Tapped(object sender, EventArgs _)
      => this._model.JumpToClickedTrack(((SmallTrackView)sender).Track, MainLogic.Instance.TrackQueue.NextUpTracks);

    private void QueuedTrackView_Tapped(object sender, EventArgs _)
      => this._model.JumpToClickedTrack(((SmallTrackView)sender).Track, MainLogic.Instance.TrackQueue.QueuedTracks);
  }
}