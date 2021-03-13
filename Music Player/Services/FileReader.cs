using Music_Player.Helpers;
using Music_Player.Interfaces;
using Music_Player.Models;
using System.Collections.Generic;
using System.Linq;
using TagLib;
using Xamarin.Forms;
using File = Java.IO.File;

namespace Music_Player.Services {
  public class FileReader {    

    private static readonly string[] _supportedFormats
      = new string[] { ".mp3", ".aac", ".ogg", ".wma", ".alac", ".pcm", ".flac", ".wav" };

    private static SerializableTrack _CreateSerialTrack(File file) {
      if (_TryReadWithTagLib(file, out var track))
        return track;

      return new SerializableTrack { Title = file.Name, Path = file.Path };
    }

    public static SerializableTrack[] CreateSerialTracks() {
      var settings = Settings.Instance;
      var nativeFeatures = DependencyService.Get<INativeFeatures>();
      var path = settings.MusicDirectory;

      if (!nativeFeatures.DirectoryExists(path))
        return new SerializableTrack[0];

      var files = nativeFeatures.EnumerateFiles3(path).Where(file => _supportedFormats.Any(format => file.Name.EndsWith(format))).ToList();

      if (files.Count == 0)
        return new SerializableTrack[0];

      var serialTracks = files.Select(f => _CreateSerialTrack(f)).ToArray();
      CacheManager.CacheTracks(serialTracks);
      settings.ReadFromCache = true;
      return serialTracks;
    }

    private static bool _TryReadWithTagLib(File file, out SerializableTrack track) {
      track = new SerializableTrack();

      try {
        var tFile = TagLib.File.Create(file.Path);
        var fileTags = tFile.Tag;     

        var title = fileTags.Title;
        track.Title = title.IsNullOrEmpty() ? file.Name : title.Trim();
        track.Path = file.Path;
        track.Duration = tFile.Properties.Duration;       
        track.Album = fileTags.Album;
        track.Artists = _GetArtists(fileTags);
        track.Genres = _GetGenres(fileTags.Genres);

        return true;
      } catch { return false; } 
    }

    private static string[] _GetArtists(Tag fileTags) {
      //artists
      var artists = fileTags.Performers; //todo: check if performers or composers have values as well
      if (artists.Length == 0) {
        var joinedArtists = fileTags.JoinedPerformers;
        if (joinedArtists != null)
          artists = joinedArtists.Split('&').Select(a => a.Trim()).ToArray();
      }

      return artists;
    }

    private static string[] _GetGenres(string[] tGenres) {
      if (tGenres.Length > 0) {
        var genreList = new List<string>();

        foreach (var g in tGenres) {
          var splitted = g.Split(Genre.SEPARATOR);
          foreach (var s in splitted)
            genreList.Add(s.Trim());
        }
        return genreList.ToArray();
      }
      return tGenres;
    }

  }
}
