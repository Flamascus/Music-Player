using Music_Player.Services;

namespace Music_Player.ViewModels {

  public class SettingsViewModel {
    private readonly Settings _settings = Settings.Instance;

    public string MusicDirectory {
      get => _settings.MusicDirectory;
      set => _settings.MusicDirectory = value;
    }
  }
}
