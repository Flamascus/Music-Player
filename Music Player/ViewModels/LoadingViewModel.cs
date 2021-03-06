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
      Task.Run(() => SendSignal());
    }

    public void SendSignal() {
      while (!_finished) {
        Task.Delay(50);
        OnPropertyChanged(nameof(Progress));
      }
    }

    public event EventHandler ProgressFinished;
  }
}
