using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.Pages {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class StoragePicker : ContentPage {
    public StoragePicker() {
      this.InitializeComponent();
    }

    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e) {
      var dir = new DirectoryInfo(this.ViewModel.StoragePaths[e.ItemIndex]);
      this.Navigation.PushAsync(new DirectoryPickerPage(dir));
    }
  }
}