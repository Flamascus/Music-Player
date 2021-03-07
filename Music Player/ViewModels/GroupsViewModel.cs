using Music_Player.Interfaces;
using Music_Player.Services;
using System;
using System.Collections.Generic;

namespace Music_Player.ViewModels {
  class GroupsViewModel : ANotifyPropertyChanged, ILoadable {
    private List<IDisplayGroup> _groups;

    public event EventHandler<EventArgs> FinishedLoading;

    public List<IDisplayGroup> Groups {
      get => this._groups;
      set {
        this._groups = value;
        this.OnPropertyChanged();
      }
    }

    public bool IsLoading {
      get => this._isLoading;
      protected set {
        this._isLoading = value;
        if (!value)
          this.FinishedLoading?.Invoke(this, new EventArgs());
      }
    }

    private bool _isLoading;

    public GroupsViewModel(List<IDisplayGroup> groups) {
      this.Groups = groups;
    }

  }
}

//