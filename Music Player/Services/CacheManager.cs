using Music_Player.Interfaces;
using Music_Player.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Music_Player.Services {
  public static class CacheManager {
    private const string _CACHE_FILE_NAME = "trackCache2.txt";
    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();

    public static void CacheTracks(List<ITrack> tracks) {
      var serialTracks = new SerializableTrack[tracks.Count];
      for (var i = 0; i < tracks.Count; ++i)
        serialTracks[i] = SerializableTrack.FromTrack(tracks[i]);

      var serializedObjects = JsonConvert.SerializeObject(serialTracks);
      _nativeFeatures.WriteAppFile(_CACHE_FILE_NAME, serializedObjects);
    }

    public static bool TryReadCache(out List<ITrack> tracks) {
      tracks = null;
      var builder = DependencyService.Get<ITrack>();
      var text = _nativeFeatures.ReadAppFile(_CACHE_FILE_NAME);
      var serialTracks = JsonConvert.DeserializeObject<SerializableTrack[]>(text);


      if (serialTracks == null)
        return false;

      tracks = serialTracks.Select(s => s.ToTrack()).ToList();
      return true;
    }



  }
}
