using Music_Player.Views.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class EmptyPage : ContentPage {
    public EmptyPage(ContentView content, string title) {
      this.Title = title;
      this.InitializeComponent();
      //NavigationPage.SetTitleView(this, new BottomTrackView());
      this.Grid.Children.Add(content, 0, 0);
    }
  }
}