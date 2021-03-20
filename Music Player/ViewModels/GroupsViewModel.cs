using Music_Player.Enums;
using Music_Player.Helpers;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.ViewModels {
  class GroupsViewModel : ALoadableNotifyPropertyChanged {   

    public GroupType GroupType {
      get => this._groupType;
      set {
        this._groupType = value;
        this._SetGroupType(value);
      }
    }

    public List<IDisplayGroup> Groups {
      get => this._groups;
      set {
        this._groups = value;
        this.OnPropertyChanged();
      }
    }

    public DisplayState DisplayState {
      get => this._displayState;
      private set {
        this._displayState = value;
        this.OnPropertyChanged();
      }
    }

    private GroupType _groupType;
    private DisplayState _displayState;
    private List<IDisplayGroup> _groups = new List<IDisplayGroup>();
    private bool _subscribedToEvents;

    private void _SetGroupType(GroupType groupType) {
      var _setGroupTypeGeneric = groupType switch {
        GroupType.Artists => (Action)(() => this._SetGroupTypeGeneric(ArtistList.Instance)),
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
        this.DisplayState = DisplayState.Loading;
      else {
        this.Groups = list.Select(g => (IDisplayGroup)g).ToList();
        this.DisplayState = this.Groups.Count == 0
          ? DisplayState.Empty
          : DisplayState.DisplayingContent;
      }
    }

    private void _List_StartedLoading(object sender, EventArgs e) {
      this.DisplayState = DisplayState.Loading;
    }

    //todo: not unsubscribing from event atm
    private void _FinishedLoading(object sender, EventArgs e) => this._SetGroupType(this._groupType);
  }
}