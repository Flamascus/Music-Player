using Music_Player.Services;
using Music_Player.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SearchPage : ContentPage {

    private readonly SongsViewModel _model;

    public SearchPage() {
      this._model = new SongsViewModel();
      this.BindingContext = this._model;
      InitializeComponent();
    }

    //todo: put into model isntead
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) {
      var text = e.NewTextValue.ToLower();
      var logic = MainLogic.Instance;

      if (text == string.Empty) {
        this._model.Tracks = logic.AllTracks;
        return;
      }

      var genres = logic.AllGenres.Where(g => g.GenreName.Contains(text)).ToList();
      var songs = logic.AllTracks.Where(t =>
      t.Title.ToLower().Contains(text)
      || t.Producer.ToLower().Contains(text)
      || t.CombinedGenreName.ToLower().Contains(text)
      ).ToList();

      this._model.Tracks = songs;
    }

    
  }
}