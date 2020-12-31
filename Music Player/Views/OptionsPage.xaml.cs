using Music_Player.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class OptionsPage : ContentPage {
    public OptionsPage(ITrack track) {
      this.Title = track.Title;
      this.InitializeComponent();
    }
  }
}