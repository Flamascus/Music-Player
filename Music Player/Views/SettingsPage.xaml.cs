using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {

    //todo: for some reason on page creation the viewmodel setters get called //todo: check if this bug still exists
    public SettingsPage() {
      this.InitializeComponent();
    }

    private void _SelectFolderClicked(object sender, EventArgs e) {
      var picker = new DirectoryPickerPage();
      picker.DirectorySelected += this._Picker_DirectorySelected;
      this.Navigation.PushAsync(picker);
    }

    private void _Picker_DirectorySelected(object sender, EventArgs e) {
      this.ViewModel.UpdateDirectory();
    }
  }
}