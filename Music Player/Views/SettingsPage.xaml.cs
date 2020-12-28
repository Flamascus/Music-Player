using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {
    public SettingsPage() {
      this.BindingContext = new SettingsViewModel();
      this.InitializeComponent();
    }
  }
}