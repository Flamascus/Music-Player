using Music_Player.Services;

namespace Music_Player.ViewModels {

  public class SettingsViewModel : ANotifyPropertyChanged {

    public bool ViewInitialized { get; set; }

    private readonly Settings _settings = Settings.Instance;

    public string MusicDirectory => this._settings.MusicDirectory;

    public bool SendReportsEnabled {
      get => this._settings.SendReportsEnabled;
      set {
        if (this.ViewInitialized)
          this._settings.SendReportsEnabled = value;
      }
    }

    public void UpdateDirectory() {     
      this.OnPropertyChanged(nameof(this.MusicDirectory));
    }


  }
}
