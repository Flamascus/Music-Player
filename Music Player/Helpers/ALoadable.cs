using Music_Player.Interfaces;
using System;

namespace Music_Player.Helpers {
  public class ALoadable : ILoadable {

    public event EventHandler<EventArgs> FinishedLoading;

    public bool IsLoading {
      get => this._isLoading;
      protected set {
        this._isLoading = value;
        if (!value)
          this.FinishedLoading?.Invoke(this, new EventArgs());
      }
    }

    private bool _isLoading = true;
  }
}
