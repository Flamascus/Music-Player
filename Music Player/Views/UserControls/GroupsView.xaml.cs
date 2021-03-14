using Music_Player.Enums;
using Music_Player.Interfaces;
using Music_Player.Models;
using Xamarin.Forms;
using System;
using Xamarin.Forms.Xaml;
using System.Linq;
using Music_Player.Models.Collections;
using Music_Player.Helpers;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GroupsView : ContentView {

    private bool _subscribedToEvents;

    public GroupType GroupType {
      get => this._groupType;
      set {
        this._groupType = value;
        this._SetGroupType(value);
      }
    }

    private GroupType _groupType;

    public GroupsView() {
      this.InitializeComponent();
    }

    private void _SetGroupType(GroupType groupType) {
      var _setGroupTypeGeneric = groupType switch {
        GroupType.Artists => (Action)(() => this._SetGroupTypeGeneric(GenreList.Instance)),
        GroupType.Genres => () => this._SetGroupTypeGeneric(GenreList.Instance),
        GroupType.Albums => () => this._SetGroupTypeGeneric(AlbumList.Instance),
        GroupType.Playlists => () => this._SetGroupTypeGeneric(PlaylistList.Instance),
        GroupType.Folders => throw new NotImplementedException(),
        _ => throw new ArgumentOutOfRangeException()
      };

      _setGroupTypeGeneric?.Invoke();
    }

    private void _SetGroupTypeGeneric<T>(LoadableList<T> list) where T : IDisplayGroup {
      if (!this._subscribedToEvents) {
        list.StartedLoading += this._List_StartedLoading;
        list.FinishedLoading += this._FinishedLoading;
        this._subscribedToEvents = true;
      }

      if (list.IsLoading)
        this._ShowLoading();
      else
        this.ViewModel.Groups = list.Select(g => (IDisplayGroup)g).ToList();
    }

    private void _List_StartedLoading(object sender, EventArgs e) {
      Device.BeginInvokeOnMainThread(() => this._ShowLoading());
    }


    //todo: not unsubscribing from event atm
    private void _FinishedLoading(object sender, EventArgs e) {
      Device.BeginInvokeOnMainThread(() => this._ShowLoading(false));
      this._SetGroupType(this._groupType);
    }

    private void _ShowLoading(bool isLoading = true) {
      this.activityIndicator.IsRunning = isLoading;
      this.activityIndicator.IsVisible = isLoading;
      this.lvGroups.IsVisible = !isLoading;
    }

    public void LvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;

      var group = (IDisplayGroup)lv.SelectedItem;
      this.Navigation.PushAsync(new GroupPage(group.Tracks, group.Name));
    }
  }
}