using Music_Player.Views.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SearchPage : ContentPage {

    private SongsView _songsView;

    public SearchPage() {
      this.InitializeComponent();
      this._songsView = new SongsView(); //todo: check if this can be done in xaml
      this.stackLayout.Children.Add(this._songsView);
    }

    //todo: put into model isntead
    private void _SearchBar_TextChanged(object sender, TextChangedEventArgs e) {
      this.ViewModel.Search(e.NewTextValue.ToLower());
      this._UpdateTrackList();
    }

    private void _UpdateTrackList() {
      this.stackLayout.Children.Remove(this._songsView);
      this._songsView.Tracks = this.ViewModel.Tracks;
      this.stackLayout.Children.Add(this._songsView);
    }

    
  }
}