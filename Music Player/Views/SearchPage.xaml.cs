using Music_Player.Interfaces;
using Music_Player.Services;
using Music_Player.ViewModels;
using Music_Player.Views.UserControls;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SearchPage : ContentPage {

    private SongsView _songsView;
    private readonly SearchViewModel _model;

    public SearchPage() {
      this._model = new SearchViewModel();
      this.BindingContext = this._model;
      this.InitializeComponent();
      this._songsView = new SongsView();

      this.stackLayout.Children.Add(this._songsView);
    }

    //todo: put into model isntead
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) {
      this._model.Search(e.NewTextValue.ToLower());
      this.UpdateTrackList();
    }

    private void UpdateTrackList() {
      this.stackLayout.Children.Remove(this._songsView);
      this._songsView = new SongsView(this._model.Tracks);
      this.stackLayout.Children.Add(this._songsView);
    }

    
  }
}