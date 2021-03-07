using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Music_Player {
  public partial class AppShell : Shell {
    public AppShell() {
      this.InitializeComponent();

      DependencyService.Get<INativeFeatures>().RequestPerimissions();


      Task.Run(() => {
        TrackList.Instance.Init();
        GenreList.Instance.Init();
        ArtistList.Instance.Init();
      });

      //Navigation.PushAsync(new LoadingPage());
    }

    private async void OnMenuItemClicked(object sender, EventArgs e) {
      await Shell.Current.GoToAsync("//LoginPage");
    }
  }
}
