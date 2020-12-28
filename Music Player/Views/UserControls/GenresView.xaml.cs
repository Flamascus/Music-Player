using Music_Player.Models;
using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GenresView : ContentView {

    public GenresView() {
      this.InitializeComponent();
      this.BindingContext = new GenresViewModel();
    }

    public void lvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;
      var genre = (Genre)lv.SelectedItem;
      this.Navigation.PushAsync(new EmptyPage(new SongsView(genre.Tracks), genre.GenreName));
    }
  }
}