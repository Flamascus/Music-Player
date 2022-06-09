using System;
using System.IO;
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

    public DirectoryPickerPage(DirectoryInfo dir) {
      this.InitializeComponent(); //todo: initializes viewModel twice
      this.ViewModel.CurrentDirectory = dir;
      //this.ViewModel = new DirectoryPickerModel(dir);
    }

    private void _ListView_ItemTapped(object sender, ItemTappedEventArgs e) {
      this.ViewModel.GoToChild(e.ItemIndex);
    }

    private void _SelectThisDirectory(object sender, EventArgs e) {
      Settings.Instance.MusicDirectory = this.ViewModel.CurrentDirectory.FullName;
      this.DirectorySelected?.Invoke(this, new EventArgs());
      Application.Current.MainPage.Navigation.PopAsync();
    }
  }
}