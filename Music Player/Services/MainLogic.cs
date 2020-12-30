using MediaManager;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.ViewModels;
using Music_Player.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Music_Player.Services {

  //todo: clear mix of logic and ui

  public class MainLogic {
    public static MainLogic Instance {
      get {
        if (_instance == null)
          _instance = new MainLogic();

        return _instance;
      }
    }
    private static MainLogic _instance;

    public List<ITrack> AllTracks { get; private set; }
    public IList<Genre> AllGenres { get; private set; }
    public TrackQueue TrackQueue { get; private set; }

    public float Progress { get; private set; }

    public Settings Settings { get; } = new Settings();
    public TrackViewModel TrackViewModel {
      get {
        if (_trackViewModel == null)
          _trackViewModel = new TrackViewModel();

        return _trackViewModel;
      }
    }
    private TrackViewModel _trackViewModel;

    private static readonly INativeFeatures _nativeFeatures = DependencyService.Get<INativeFeatures>();
    public INavigation Navigation { get; set; }
    
    private static readonly string[] _supportedFormats = new string[] { ".mp3", ".aac", ".ogg", ".wma", ".alac", ".pcm", ".flac", ".wav" };   

    public MainLogic() {
      _nativeFeatures.RequestPerimissions();
    }

    public async void InitAsync() {
      await Task.Run(() => {
        var tracks = _CreateTrackList();

        this.AllTracks = tracks;
        this.TrackQueue = new TrackQueue(tracks);
        this.AllGenres = _CreateGenreList();
        //this.AllGenres = tracks.GroupBy(t => t.GenreNames[0])
        //  .Select(group => new Genre(group.Key, group.ToList())).OrderBy(g => g.GenreName).ToList();    
      });
    }

    private List<Genre> _CreateGenreList() {
      var tracks = this.AllTracks;
      var allGenreNames = new List<string>();

      foreach (var track in tracks)
        foreach (var genre in track.GenreNames) {
          if (!allGenreNames.Any(g => g.Equals(genre, StringComparison.OrdinalIgnoreCase)))
            allGenreNames.Add(genre);
        }

      allGenreNames = allGenreNames.ToList();
      allGenreNames.Sort();
      var genres = allGenreNames.Select(g => new Genre(g, tracks.Where(t => t.GenreNames.ToList().Contains(g)).ToList())).ToList();

      return genres;
    }

    private List<ITrack> _CreateTrackList() {
      Task.Delay(200); //todo: dunno why this is needed
      var lines = _nativeFeatures.ReadAllLinesAppFile("trackCache.txt");

      if (lines.Length > 1 && this.Settings.ReadFromCache) {
        this.Progress = 1;
        return _ReadTrackCache(lines);
      }

      this.Navigation.PushAsync(new LoadingPage());

      var nativeFeatures = _nativeFeatures;
      //var path = nativeFeatures.MusicLibaryPath + "/music4phone" + "/folder 28";
      var path = this.Settings.MusicDirectory;
      var files = nativeFeatures.EnumerateFiles3(path).Where(file => _supportedFormats.Any(format => file.Name.EndsWith(format))).ToList();
      var trackBuilder = DependencyService.Get<ITrack>();
      var tracks = new List<ITrack>();
      var count = files.Count;

      for (var i = 0; i < files.Count; ++i) {
        this.Progress = (float)i / count;
        tracks.Add(trackBuilder.Create(files[i]));
      }
      this.Progress = 1;

      tracks = tracks.OrderBy(t => t.Title).ToList();
      _CacheTracks(tracks);

      return tracks;
    }

    private void _CacheGenres() {
      var sb = new StringBuilder();

      foreach (var genre in this.AllGenres) {
        sb.AppendLine("*" + genre.GenreName);
        genre.Tracks.ForEach(t => sb.AppendLine(t.Path));
      }

      _nativeFeatures.WriteAppFile("genreCache.txt", sb.ToString()); //todo: make string to constant 
    }

    //todo: use json and serializable track instead
    private void _CacheTracks(IList<ITrack> tracks) {
      var sb = new StringBuilder();

      foreach (var track in tracks) {
        var genres = track.GenreNames;
        var last = genres[genres.Length - 1];
        var genreString = string.Empty;

        //create genre string
        foreach (var genre in track.GenreNames) {
          genreString += genre;
          if (genre != last)
            genreString += "/";
        }

        sb.AppendLine(track.Path);
        sb.AppendLine(track.Title);
        sb.AppendLine(track.Producer);
        sb.AppendLine(genreString);
      }

      this.Settings.ReadFromCache = true;
      _nativeFeatures.WriteAppFile("trackCache.txt", sb.ToString());
    }

    private void _ReadGenreCache() {

    }

    private static List<ITrack> _ReadTrackCache(string[] lines) {
      var tracks = new List<ITrack>();
      var builder = DependencyService.Get<ITrack>();

      for (var i = 0; i < lines.Count(); i += 4) { //todo: should be handled with serialization
        tracks.Add(builder.Create(
          lines[i],
          lines[i + 1],
          lines[i + 2],
          lines[i + 3].Split('/')
          ));
      }

      return tracks;
    }

  }
}
