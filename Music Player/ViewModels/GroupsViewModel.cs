using Music_Player.Helpers;
using Music_Player.Interfaces;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  class GroupsViewModel : ALoadableNotifyPropertyChanged {
    private List<IDisplayGroup> _groups = new List<IDisplayGroup>();

    public List<IDisplayGroup> Groups {
      get => this._groups;
      set {
        this._groups = value;
        this.OnPropertyChanged();
      }
    }

  }
}