using Music_Player.Interfaces;
using System;

namespace Music_Player.Models.DisplayGroup {
  public abstract class ALoadableDisplayGroup : ADisplayGroup, ILoadable {

    public event EventHandler<EventArgs> StartedLoading;
    public event EventHandler<EventArgs> FinishedLoading;

    public bool IsLoading {
      get => this._isLoading;
      set {
        this._isLoading = value;
        if (value)
          this.StartedLoading?.Invoke(this, new EventArgs());
        else
          this.FinishedLoading?.Invoke(this, new EventArgs());
      }
    }

    private bool _isLoading;
  }
}