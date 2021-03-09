﻿using Music_Player.Helpers;
using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Music_Player.Models {

  //todo: make array or readonly list
  public class TrackList : AReadOnlyList<ITrack> {

    public static TrackList Instance = new TrackList();

    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();

    private static readonly string[] _supportedFormats
      = new string[] { ".mp3", ".aac", ".ogg", ".wma", ".alac", ".pcm", ".flac", ".wav" };

    private TrackList() { }

    public void Init() {
      this.items = (Settings.Instance.ReadFromCache && CacheManager.TryReadTrackCache(out var tracks))
        ? tracks
        : this._CreateTrackListFromFiles();

      this.IsLoading = false;
    }

    private List<ITrack> _CreateTrackListFromFiles() {
      var settings = Settings.Instance;
      var nativeFeatures = _nativeFeatures;
      var path = settings.MusicDirectory;

      if (!nativeFeatures.DirectoryExists(path))
        return new List<ITrack>();

      var files = nativeFeatures.EnumerateFiles3(path).Where(file => _supportedFormats.Any(format => file.Name.EndsWith(format))).ToList();

      if (files.Count == 0)
        return new List<ITrack>();

      var trackBuilder = DependencyService.Get<ITrack>();
      var tracks = new List<ITrack>();
      var count = files.Count;

      foreach (var file in files)
        tracks.Add(trackBuilder.Create(file));

      tracks = tracks.OrderBy(t => t.Title).ToList();
      CacheManager.CacheTracks(tracks);
      settings.ReadFromCache = true;   

      return tracks;
    }

  }
}
