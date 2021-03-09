using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Models.Collections;
using Music_Player.Models.DisplayGroup;
using Music_Player.Models.Serializable;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Music_Player.Services {

  //todo: alot of double code, can probably make this better
  public static class CacheManager {
    private const string _TRACK_CACHE_FILE_NAME = "trackCache.txt";
    private const string _QUEUE_FILE_NAME = "queueCache.txt";
    private const string _PLAYLIST_FILE_NAME = "playlists.txt";

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

      if (text == null)
        return false;

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

      if (text == null)
        return false;

      var serializedQueue = JsonConvert.DeserializeObject<SerializableTrackQueue>(text);
 
      if (serializedQueue == null)
        return false;

      serializedQueue.SetTrackQueue();
      return true;
    }

    public static void CachePlaylists() {
      var playlists = PlaylistList.Instance;
      var serialPlaylists = new SerializablePlaylist[playlists.Count];

      for (var i = 0; i < playlists.Count; ++i)
        serialPlaylists[i] = SerializablePlaylist.FromPlaylist(playlists[i]);

      var serializedObjects = JsonConvert.SerializeObject(serialPlaylists);
      _nativeFeatures.WriteAppFile(_PLAYLIST_FILE_NAME, serializedObjects);
    }

    //todo: not using try atm so could just be normal method?
    public static bool TryReadPlaylistCache(out List<Playlist> playlists) {
      playlists = new List<Playlist>();
      var text = _nativeFeatures.ReadAppFile(_PLAYLIST_FILE_NAME);

      if (text == null)
        return false;

      var serializedPlaylists = JsonConvert.DeserializeObject<SerializablePlaylist[]>(text);

      if (serializedPlaylists == null)
        return false;

      playlists = serializedPlaylists.Select(p => p.ToPlaylist()).ToList();
      return true;
    }

  }
}
