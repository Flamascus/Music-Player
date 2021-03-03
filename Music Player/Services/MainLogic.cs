using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.ViewModels;
using Music_Player.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public List<Genre> AllGenres { get; private set; }
    public List<Artist> AllArtists { get; private set; }
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
        var tracks = this._CreateTrackList();
        this.AllTracks = tracks;
        this.TrackQueue = new TrackQueue(tracks);
        this.AllGenres = this._CreateGenreList();
        this.AllArtists = this._CreateArtistList();
        this.Progress = 1;
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
      var genres = allGenreNames.Select(g => new Genre(g, tracks.Where(t => t.GenreNames.Contains(g)).ToList())).ToList();

      return genres;
    }

    private List<Artist> _CreateArtistList() {
      var tracks = this.AllTracks;
      var allArtistNames = new List<string>();

      foreach (var track in tracks)
        foreach (var artist in track.ArtistNames) {
          if (!allArtistNames.Any(g => g.Equals(artist, StringComparison.OrdinalIgnoreCase)))
            allArtistNames.Add(artist);
        }

      allArtistNames = allArtistNames.ToList();
      allArtistNames.Sort();
      var artists = allArtistNames.Select(g => new Artist(g, tracks.Where(t => t.ArtistNames.Contains(g)).ToList())).ToList();

      return artists;
    }

    private List<ITrack> _CreateTrackList() {
      Task.Delay(200); //todo: dunno why this is needed

      //var lines = _nativeFeatures.ReadAllLinesAppFile("trackCache.txt");

      if (this.Settings.ReadFromCache && CacheManager.TryReadCache(out var tracks))    
        return tracks;

      this.Navigation.PushAsync(new LoadingPage());

      return _CreateTrackListFromFiles();
    }

    private List<ITrack> _CreateTrackListFromFiles() {
      var nativeFeatures = _nativeFeatures;
      //var path = nativeFeatures.MusicLibaryPath + "/music4phone" + "/folder 28";
      var path = this.Settings.MusicDirectory;
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

      this.Settings.ReadFromCache = true;
      return tracks;
    }

  }
}
