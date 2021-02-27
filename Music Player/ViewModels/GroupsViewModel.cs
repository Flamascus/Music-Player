using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  class GroupsViewModel : ANotifyPropertyChanged {
    private List<IDisplayGroup> _groups;

    public List<IDisplayGroup> Groups {
      get => this._groups;
      set {
        this._groups = value;
        this.OnPropertyChanged();
      }
    }

    public GroupsViewModel(List<IDisplayGroup> groups) {
      this.Groups = groups;
    }

  }
}

//