using Music_Player.Enums;
using Music_Player.Views.UserControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {

    //todo: for some reason on page creation the viewmodel setters get called //todo: check if this bug still exists
    public SettingsPage() {
      this.InitializeComponent();
      this.ViewModel.ViewInitialized = true;
    }

    private void _SelectFolderClicked(object sender, EventArgs e) {
      var picker = new DirectoryPickerPage();
      picker.DirectorySelected += this._Picker_DirectorySelected;
      this.Navigation.PushAsync(picker);
    }

    private void _Picker_DirectorySelected(object sender, EventArgs e) {
      this.ViewModel.UpdateDirectory();
    }

    private void _SelectPrimaryColorTapped(object sender, EventArgs e) => ColorPickerPopup.Create(AppColor.Primary);

    private void _SelectAccentColorTapped(object sender, EventArgs e) => ColorPickerPopup.Create(AppColor.Accent);

    private void _LightButtonTapped(object sender, EventArgs e) => this.BtnLight.IsChecked = true;
    private void _DarkButtonTapped(object sender, EventArgs e) => this.BtnDark.IsChecked = true;
    private void _BlackButtonTapped(object sender, EventArgs e) => this.BtnBlack.IsChecked = true;
  }
}