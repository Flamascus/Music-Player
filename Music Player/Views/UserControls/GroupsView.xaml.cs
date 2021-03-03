using Music_Player.Enums;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GroupsView : ContentView {

    public GroupType TestType {
      get => this._testType;
      set {
        this._testType = value;

        var logic = MainLogic.Instance;

        while (logic.AllArtists == null || logic.AllGenres == null)
          Task.Delay(50);

          switch (value) {
          case GroupType.Artists:         
            this._model.Groups = logic.AllArtists.ConvertAll(g => (IDisplayGroup)g);
            break;
          case GroupType.Genres:
            this._model.Groups = logic.AllGenres.ConvertAll(g => (IDisplayGroup)g);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private GroupType _testType;
    private readonly GroupsViewModel _model;   

    public GroupsView() {
      this.InitializeComponent();
      var model = new GroupsViewModel(new List<IDisplayGroup>());
      this._model = model;
      this.BindingContext = model;
    }

    public void lvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;

      var group = (IDisplayGroup)lv.SelectedItem;
      this.Navigation.PushAsync(new GroupPage(group.Tracks, group.Name));
    }
  }
}