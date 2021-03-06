using Music_Player.Interfaces;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Music_Player.Services {

  //todo: implement
  public class Settings {

    public static Settings Instance = _instance == null ? _instance = new Settings() : _instance;

    private static Settings _instance;

    public string MusicDirectory {
      get => _musicDirectory;
      set {
        _musicDirectory = value;
        this.WriteSetting(nameof(MusicDirectory), value);
        this.ReadFromCache = false;
      }
    }
    //public List<string> Blacklist { get; }

    public bool ReadFromCache {
      get => _readFromCache;
      set {
        this.WriteSetting(nameof(ReadFromCache), value.ToString());
        _readFromCache = value;
      }
    }


    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();
    private string _musicDirectory;
    private bool _readFromCache;
    private const string _FILE_NAME = "settings.ini";

    private Settings() {
      var content = _ReadFile();
      if (content.Length == 0)
        this._InitSettings();

      this._ReadSettings();
    }

    private void _ReadSettings() {
      this._musicDirectory = GetSetting(nameof(MusicDirectory));
      var setting = GetSetting(nameof(ReadFromCache));
      this._readFromCache = bool.Parse(GetSetting(nameof(ReadFromCache)));
    }

    private static string[] _ReadFile() => DependencyService.Get<INativeFeatures>().ReadAllLinesAppFile(_FILE_NAME);
    private static void _SaveSettings(string[] content) {
      var sb = new StringBuilder();
      foreach (var line in content)
        sb.AppendLine(line);

      _nativeFeatures.WriteAppFile(_FILE_NAME, sb.ToString());
    }
    
    //sets default values for settings
    private void _InitSettings() {
      this.WriteSetting(nameof(MusicDirectory), _nativeFeatures.MusicLibaryPath);
      this.WriteSetting(nameof(ReadFromCache), true.ToString());
    }

    public void WriteSetting(string key, string value) {
      var settingFound = false;
      var settingString = $"{key}-{value}";
      var content = _ReadFile();

      for (var i = 0; i < content.Length; ++i) {
        if (content[i].StartsWith(key)) {
          settingFound = true;
          content[i] = settingString;
        }
      }

      if (!settingFound) {
        var list = content.ToList();
        list.Add(settingString);
        content = list.ToArray();
      }

      _SaveSettings(content);
    }

    public string GetSetting(string key) {
      var content = _ReadFile();

      for (var i = 0; i < content.Length; ++i) {
        if (content[i].StartsWith(key))
          return content[i].Split('-')[1];
      }

      return null;
    }

  }
}
