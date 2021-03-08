using Microsoft.AppCenter.Crashes;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Models.Serializable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Music_Player.Services {
  public static class CacheManager {
    private const string _TRACK_CACHE_FILE_NAME = "trackCache2.txt";
    private const string _QUEUE_FILE_NAME = "queueCache.txt";

    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();

    public static void CacheTracks(List<ITrack> tracks) {
      var serialTracks = new SerializableTrack[tracks.Count];
      for (var i = 0; i < tracks.Count; ++i)
        serialTracks[i] = SerializableTrack.FromTrack(tracks[i]);

      var serializedObjects = JsonConvert.SerializeObject(serialTracks);
      _nativeFeatures.WriteAppFile(_TRACK_CACHE_FILE_NAME, serializedObjects);
    }

    public static bool TryReadTrackCache(out List<ITrack> tracks) {
      tracks = null;
      var text = _nativeFeatures.ReadAppFile(_TRACK_CACHE_FILE_NAME);
      var serialTracks = JsonConvert.DeserializeObject<SerializableTrack[]>(text);


      if (serialTracks == null)
        return false;

      tracks = serialTracks.Select(s => s.ToTrack()).ToList();
      return true;
    }

    public static void CacheQueue() {
      var serializedQueue = SerializableTrackQueue.Create();
      var serializedObject = JsonConvert.SerializeObject(serializedQueue);
      _nativeFeatures.WriteAppFile(_QUEUE_FILE_NAME, serializedObject);
    }

    public static bool TryReadQueueCache() {
      var text = _nativeFeatures.ReadAppFile(_QUEUE_FILE_NAME);
      var serializedQueue = JsonConvert.DeserializeObject<SerializableTrackQueue>(text);

      if (serializedQueue == null)
        return false;

      serializedQueue.SetTrackQueue();
      return true;
    }

  }
}
