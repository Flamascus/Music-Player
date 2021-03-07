using System;

namespace Music_Player.Interfaces {
  interface ILoadable {
    event EventHandler<EventArgs> FinishedLoading;
    bool IsLoading { get; }
  }
}
