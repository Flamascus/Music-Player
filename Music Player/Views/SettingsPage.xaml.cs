using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SettingsPage : ContentPage {

    //todo: for some reason on page creation the viewmodel setters get called
    public SettingsPage() {
      this.BindingContext = new SettingsViewModel();
      this.InitializeComponent();  
    }
  }
}