using Music_Player.Models;
using Music_Player.Services;
using System;
using System.Threading.Tasks;

namespace Music_Player.ViewModels {
  class LoadingViewModel : ANotifyPropertyChanged {

    private bool _finished;
    public float Progress {
      get {
        var progress = TrackList.Instance.Progress;
        if (progress == 1 && !this._finished) {
          this._finished = true;
          ProgressFinished?.Invoke(null, null);
        }

        return progress;
      }
    }

    public LoadingViewModel() {
      //_logic.InitAsync();
      Task.Run(() => this.SendSignal());
    }

    public void SendSignal() {
      while (!this._finished) {
        Task.Delay(50);
        this.OnPropertyChanged(nameof(this.Progress));
      }
    }

    public event EventHandler ProgressFinished;
  }
}
