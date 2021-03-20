using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Music_Player.Services;
using Music_Player.ViewModels;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]

  //todo: make into more general folder selector
  public partial class DirectoryPickerPage : ContentPage {

    public event EventHandler<EventArgs> DirectorySelected;

    public DirectoryPickerPage() {
      this.InitializeComponent();
    }

    private void _ListView_ItemTapped(object sender, ItemTappedEventArgs e) {
      this.ViewModel.GoToChild(e.ItemIndex);
    }

    private void _SelectThisDirectory(object sender, EventArgs e) {
      Settings.Instance.MusicDirectory = this.ViewModel.CurrentDirectory.FullName;
      this.DirectorySelected?.Invoke(this, new EventArgs());
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}