using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class QueuePage : ContentPage {
    public QueuePage() {
      this.InitializeComponent();

      this.BindingContext = new QueueViewModel();
    }
  }
}