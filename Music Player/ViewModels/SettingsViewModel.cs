using Music_Player.Services;

namespace Music_Player.ViewModels {

  public class SettingsViewModel {
    private readonly Settings _settings = Settings.Instance;

    public string MusicDirectory {
      get => this._settings.MusicDirectory;
      set => this._settings.MusicDirectory = value;
    }

    public bool SendReportsEnabled {
      get => this._settings.SendReportsEnabled;
      set => this._settings.SendReportsEnabled = value;
    }
  }
}
