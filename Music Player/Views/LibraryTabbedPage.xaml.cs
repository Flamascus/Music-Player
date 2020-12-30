using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LibraryTabbedPage : ContentPage {

    public LibraryTabbedPage() {
      Thread.Sleep(200); //todo: dunno why this is needed
      this.InitializeComponent();
    }

    private void _SearchClicked(object _, EventArgs __) => this.Navigation.PushAsync(new SearchPage());
  }
}