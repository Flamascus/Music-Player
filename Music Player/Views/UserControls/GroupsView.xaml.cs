using Music_Player.Enums;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GroupsView : ContentView {

    public GroupType GroupType {
      get => this._groupType;
      set {
        this._groupType = value;
        this._SetGroupType(value);
      }
    }

    private GroupType _groupType;
    private readonly GroupsViewModel _model;

    public GroupsView() {
      this.InitializeComponent();
      var model = new GroupsViewModel(new List<IDisplayGroup>());
      this._model = model;
      this.BindingContext = model;
    }

    private void _SetGroupType(GroupType groupType) {
      var artists = ArtistList.Instance;
      var genres = GenreList.Instance;

      switch (groupType) {
        case GroupType.Artists:
          if (artists.IsLoading) {
            this._ShowLoading();
            artists.FinishedLoading += this._FinishedLoading;
          } else
            this._model.Groups = artists.Select(g => (IDisplayGroup)g).ToList();
          break;

        case GroupType.Genres:
          if (genres.IsLoading) {
            this._ShowLoading();
            genres.FinishedLoading += this._FinishedLoading;
          } else
            this._model.Groups = genres.Select(g => (IDisplayGroup)g).ToList();
          break;

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    //todo: not unsubscribing from event atm
    private void _FinishedLoading(object sender, EventArgs e) {
      this._ShowLoading(false);
      this._SetGroupType(this._groupType);
    }

    private void _ShowLoading(bool isLoading = true) {
      this.activityIndicator.IsRunning = isLoading;
      this.activityIndicator.IsVisible = isLoading;
      this.lvGroups.IsVisible = !isLoading;
    }

    public void lvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;

      var group = (IDisplayGroup)lv.SelectedItem;
      this.Navigation.PushAsync(new GroupPage(group.Tracks, group.Name));
    }
  }
}