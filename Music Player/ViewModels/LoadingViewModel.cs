using Music_Player.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Music_Player.ViewModels {
  class LoadingViewModel : INotifyPropertyChanged {
    private MainLogic _logic = MainLogic.Instance;

    private bool _finished;
    public float Progress {
      get { 
        var progress = this._logic.Progress;
        if (progress == 1) {
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

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
