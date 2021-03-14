using System;

namespace Music_Player.Interfaces {
  interface ILoadable {

    public event EventHandler<EventArgs> StartedLoading;
    event EventHandler<EventArgs> FinishedLoading;
    bool IsLoading { get; }
  }
}
