using Music_Player.Interfaces;
using Music_Player.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

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

    public void _LvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;
      this._model.OnTrackTapped((ITrack)lv.SelectedItem);
    }

    //todo: defo an awful way of doing this.. but works ¯\_(ツ)_ /¯
    //also creates problems if multiple tracks have the same title
    private void _OptionsTapped(object sender, EventArgs e) {
      var grid = (Grid)((Image)sender).Parent;
      var title = ((Label)grid.Children.Select(c => c.FindByName("lblTitle")).FirstOrDefault()).Text;
      var track = this.lvTracks.ItemsSource.Cast<ITrack>().Where(t => t.Title == title).FirstOrDefault();
      this.Navigation.PushAsync(new OptionsPage(track));
    }
  }
}