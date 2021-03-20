using System;
using Music_Player.Enums;
using Music_Player.Models;
using Music_Player.Views.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Music_Player.Views.UserControls.SmallTrackView;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class QueuePage : ContentPage {

    public QueuePage() {
      this.InitializeComponent();
    }

    private void _NextUpTrackView_Tapped(object sender, EventArgs _)
      => TrackQueue.Instance.JumpToNextUpTrack(((SmallTrackView)sender).Track);

    private void _QueuedTrackView_Tapped(object sender, EventArgs _)
      => TrackQueue.Instance.JumpToQueueTrack(((SmallTrackView)sender).Track);


    private async void _OptionsTapped(object sender, OptionsEventArgs e) {
      await TrackOptions.DisplaySpecialOptionsAsync(e.Track, TrackOption.RemoveFromQueue);
      this.ViewModel.Refresh();
    }

    private async void _CurrentTrackOptionsTapped(object sender, OptionsEventArgs e)
      => await TrackOptions.DisplayBasicOptionsAsync(e.Track);
  }
}