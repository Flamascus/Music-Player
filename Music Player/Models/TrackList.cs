using Music_Player.Helpers;
using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Music_Player.Models {

  //todo: make array or readonly list
  public class TrackList : AReadOnlyList<ITrack> {

    public static TrackList Instance = new TrackList();

    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();

    public float Progress { get; private set; }

    private static readonly string[] _supportedFormats
      = new string[] { ".mp3", ".aac", ".ogg", ".wma", ".alac", ".pcm", ".flac", ".wav" };

    private TrackList() { }

    public void Init() {
      Task.Delay(200); //todo: dunno why this is needed
      //var lines = _nativeFeatures.ReadAllLinesAppFile("trackCache.txt");

      this.items = (Settings.Instance.ReadFromCache && CacheManager.TryReadCache(out var tracks))
        ? tracks
        : _CreateTrackListFromFiles();

      this.Progress = 1;
    }

    private List<ITrack> _CreateTrackListFromFiles() {
      var settings = Settings.Instance;
      var nativeFeatures = _nativeFeatures;
      //var path = nativeFeatures.MusicLibaryPath + "/music4phone" + "/folder 28";
      var path = settings.MusicDirectory;
      var files = nativeFeatures.EnumerateFiles3(path).Where(file => _supportedFormats.Any(format => file.Name.EndsWith(format))).ToList();

      if (files.Count == 0) { //todo: check maybe not needed   
        return new List<ITrack>();
      }

      var trackBuilder = DependencyService.Get<ITrack>();
      var tracks = new List<ITrack>();
      var count = files.Count;

      for (var i = 0; i < files.Count; ++i) {
        this.Progress = (float)i / count;
        tracks.Add(trackBuilder.Create(files[i]));
      }

      tracks = tracks.OrderBy(t => t.Title).ToList();
      CacheManager.CacheTracks(tracks);
      settings.ReadFromCache = true;   

      return tracks;
    }


  }
}
