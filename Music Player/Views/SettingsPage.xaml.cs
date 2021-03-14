using System;
using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {

    private readonly SettingsViewModel _model;

    //todo: for some reason on page creation the viewmodel setters get called
    public SettingsPage() {
      this._model = new SettingsViewModel();
      this.BindingContext = this._model;

      this.InitializeComponent();
    }

    private void _SelectFolderClicked(object sender, EventArgs e) {
      var picker = new DirectoryPickerPage();
      picker.DirectorySelected += this._Picker_DirectorySelected;
      this.Navigation.PushAsync(picker);
    }

    private void _Picker_DirectorySelected(object sender, EventArgs e) {
      this._model.UpdateDirectory();
    }
  }
}