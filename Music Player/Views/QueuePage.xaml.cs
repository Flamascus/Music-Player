using System;
using Music_Player.Models;
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
      this.BindingContext = this._model;
    }

    private void _NextUpTrackView_Tapped(object sender, EventArgs _)
      => TrackQueue.Instance.JumpToNextUpTrack(((SmallTrackView)sender).Track);

    private void _QueuedTrackView_Tapped(object sender, EventArgs _)
      => TrackQueue.Instance.JumpToQueueTrack(((SmallTrackView)sender).Track);
  }
}