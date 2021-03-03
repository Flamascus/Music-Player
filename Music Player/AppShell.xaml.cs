using Music_Player.Services;
using System;
using Xamarin.Forms;

namespace Music_Player {
  public partial class AppShell : Shell {
    public AppShell() {
      InitializeComponent();
      var logic = MainLogic.Instance;
      logic.Navigation = this.Navigation;
      logic.InitAsync();
    }

    private async void OnMenuItemClicked(object sender, EventArgs e) {
      await Shell.Current.GoToAsync("//LoginPage");
    }
  }
}
