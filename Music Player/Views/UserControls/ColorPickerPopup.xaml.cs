using Music_Player.Enums;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ColorPickerPopup : ContentView {

    public static SfPopupLayout Create(AppColor appColor) {
      var popup = new ColorPickerPopup(appColor).popUpLayout;
      popup.Show();
      return popup;
    }

    public ColorPickerPopup(AppColor appColor) {
      this.InitializeComponent();
      var colorPickerView = new ColorPickerView(appColor);
      this.popUpLayout.PopupView.ContentTemplate = new DataTemplate(() => colorPickerView);
    }
  }
}