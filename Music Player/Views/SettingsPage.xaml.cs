using Music_Player.Views.UserControls;
using Syncfusion.XForms.PopupLayout;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {

    private SfPopupLayout _popup;

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

    private void _SelectPrimaryColorTapped(object sender, EventArgs e) {
      var color = this.BackgroundColor; //todo: get these from resources
      var textColor = Color.White;

      var popup = new SfPopupLayout();
      this._popup = popup;
      this._SetStyle();
      var colorPickerView = new ColorPickerView();
      colorPickerView.ColorSelected += _ColorPickerView_ColorSelected;

      popup.PopupView.ContentTemplate = new DataTemplate(() => colorPickerView);
      popup.Show();
    }

    private void _SetStyle() {
      var resources = App.Current.Resources;
      var primaryColor = (Color)resources["Primary"];
      var textColor = (Color)resources["Text"];
      var bgColor = (Color)resources["Back"];
      this._popup.PopupView.PopupStyle = new PopupStyle {
        HeaderBackgroundColor = primaryColor,
        HeaderTextColor = textColor,
        AcceptButtonBackgroundColor = primaryColor,
        AcceptButtonTextColor = textColor,
        OverlayColor = bgColor,
        BorderColor = bgColor,
        FooterBackgroundColor = bgColor
      };
    }

    private void _ColorPickerView_ColorSelected(object sender, EventArgs e) {
      this._SetStyle();
    }
  }
}