using Music_Player.Interfaces;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SongsView : ContentView {

    private readonly SongsViewModel _model = new SongsViewModel();

    public SongsView() {
      var model = new SongsViewModel();
      this._model = model;
      this.BindingContext = model;      
      this.InitializeComponent();
    }

    public SongsView(List<ITrack> tracks) {
      var model = new SongsViewModel(tracks);
      this._model = model;
      this.BindingContext = model;
      this.InitializeComponent();
    }

    public List<ITrack> Tracks { get; }

    public void lvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;
      this._model.OnTrackTapped((ITrack)lv.SelectedItem);
    }
  }
}