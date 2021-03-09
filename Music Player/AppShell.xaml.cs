using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Models.Collections;
using Music_Player.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Music_Player {
  public partial class AppShell : Shell {
    public AppShell() {
      this.InitializeComponent();

      DependencyService.Get<INativeFeatures>().RequestPerimissions();

      //todo: make inits static and put together with instance creation
      Task.Run(() => {
        TrackList.Instance.Init();
        GenreList.Instance.Init();
        ArtistList.Instance.Init();
        PlaylistList.Instance.Init();
        CacheManager.TryReadQueueCache();
      });
    }

    private async void _OnMenuItemClicked(object sender, EventArgs e) {
      await Current.GoToAsync("//LoginPage");
    }
  }
}