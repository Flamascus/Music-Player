using Microsoft.AppCenter;
using Music_Player.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Music_Player.Services {

  public class Settings {

    public static Settings Instance = _instance ??= new Settings();

    private static Settings _instance;

    public string MusicDirectory {
      get => this._musicDirectory;
      set {
        this._musicDirectory = value;      
        this.WriteSetting(nameof(this.MusicDirectory), value);
        this.ReadFromCache = false;
        Task.Run(() => new DataCreator().InitData());
      }
    }
    //public List<string> Blacklist { get; }

    public bool ReadFromCache {
      get => this._readFromCache;
      set {
        this.WriteSetting(nameof(this.ReadFromCache), value);
        this._readFromCache = value;
      }
    }

    public bool SendReportsEnabled {
      get => this._sendReportsEnabled;
      set {
        this.WriteSetting(nameof(this.SendReportsEnabled), value);
        AppCenter.SetEnabledAsync(value);
        this._sendReportsEnabled = value;
      }
    }

    private string _musicDirectory;
    private bool _readFromCache;
    private bool _sendReportsEnabled;
    private const string _FILE_NAME = "settings.ini";

    private Settings() {
      var content = _ReadFile();
      if (content.Length == 0)
        this._InitSettings();

      this._ReadSettings();
    }

    private void _ReadSettings() {
      this._musicDirectory = this.GetSetting(nameof(this.MusicDirectory));
      this._readFromCache = bool.Parse(this.GetSetting(nameof(this.ReadFromCache)));
      this._sendReportsEnabled = bool.Parse(this.GetSetting(nameof(this.SendReportsEnabled)));
    }

    private static string[] _ReadFile() => DependencyService.Get<INativeFeatures>().ReadAllLinesAppFile(_FILE_NAME);
    private static void _SaveSettings(string[] content) {
      var sb = new StringBuilder();
      foreach (var line in content)
        sb.AppendLine(line);

      DependencyService.Get<INativeFeatures>().WriteAppFile(_FILE_NAME, sb.ToString());
    }

    //sets default values for settings
    private void _InitSettings() {
      this.WriteSetting(nameof(this.MusicDirectory), DependencyService.Get<INativeFeatures>().MusicLibaryPath);
      this.WriteSetting(nameof(this.ReadFromCache), true);
      this.WriteSetting(nameof(this.SendReportsEnabled), true);
    }

    public void WriteSetting(string key, object value) {
      var settingFound = false;
      var settingString = $"{key}=\"{value}\"";
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
          return content[i].Split('\"')[1];
      }

      return null;
    }

  }
}
