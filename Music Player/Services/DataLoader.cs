using Music_Player.Helpers;
using Music_Player.Models;
using Music_Player.Models.Collections;
using Music_Player.Models.DisplayGroup;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Music_Player.Services {
  public class DataLoader {
    private readonly List<Genre> _genres = new List<Genre>();
    private readonly List<Artist> _artists = new List<Artist>();
    private readonly List<Album> _albums = new List<Album>();

    public void InitData() {
      TrackList.Instance.IsLoading = true;
      GenreList.Instance.IsLoading = true;
      ArtistList.Instance.IsLoading = true;
      AlbumList.Instance.IsLoading = true;
      PlaylistList.Instance.IsLoading = true;
      Folder.IsLoading = true;
      var rootFolder = Folder.Root = new Folder(new DirectoryInfo(Settings.Instance.MusicDirectory));

      var serialTracks = this._GetSerialTracks();
      var tracks = serialTracks.Select(s => new Track(s)).ToList();

      for (var i = 0; i < serialTracks.Length; ++i) {
        var serialTrack = serialTracks[i];
        var track = tracks[i];

        this._HandleGenres(track, serialTrack.Genres);
        this._HandleArtists(track, serialTrack.Artists);
        this._HandleAlbum(track, serialTrack.Album);
        _HandleFolder(track);
      }

      Folder.IsLoading = false;
      TrackList.Instance.Init(tracks.OrderBy(t => t.Title).ToList());
      GenreList.Instance.Init(this._genres.OrderBy(g => g.Name).ToList());
      ArtistList.Instance.Init(this._artists.OrderBy(a => a.Name).ToList());
      AlbumList.Instance.Init(this._albums.OrderBy(a => a.Name).ToList());
      PlaylistList.Instance.Init();
    }

    private SerializableTrack[] _GetSerialTracks()
      => (Settings.Instance.ReadFromCache && CacheManager.TryReadTrackCache(out var cachedTracks))
        ? cachedTracks
        : FileReader.CreateSerialTracks();

    private void _HandleGenres(Track track, string[] genreNames) {
      var genres = this._genres;

      if (genreNames == null)
        track.Genres = new Genre[0];
      else {
        var trackGenres = new List<Genre>();

        foreach (var genreName in genreNames) {
          var lowerGenreName = genreName.ToLower();
          var genre = genres.FirstOrDefault(g => g.Name.ToLower() == lowerGenreName);

          if (genre == null) {
            genre = new Genre(genreName, track);
            genres.Add(genre);
          } else
            genre.Tracks.Add(track);

          trackGenres.Add(genre);
        }
        track.Genres = trackGenres.ToArray();
      }
    }

    private void _HandleArtists(Track track, string[] artistNames) {
      if (artistNames == null) {
        track.Artists = new Artist[0];
        return;
      }
      var artists = this._artists;
      var trackArtists = new List<Artist>();

      foreach (var artistName in artistNames) {
        var artistLower = artistName.ToLower();
        var artist = artists.FirstOrDefault(a => a.Name.ToLower() == artistLower);

        if (artist == null) {
          artist = new Artist(artistName, track);
          artists.Add(artist);
        } else
          artist.Tracks.Add(track);

        trackArtists.Add(artist);
      }
      track.Artists = trackArtists.ToArray();
    }

    private void _HandleAlbum(Track track, string albumName) {
      //album
      if (!albumName.IsNullOrEmpty()) {
        var albums = this._albums;
        var album = albums.FirstOrDefault(a => a.Name == albumName);

        if (album == null) {
          album = new Album(albumName, track);
          albums.Add(album);
        } else
          album.Tracks.Add(track);

        track.Album = album;
      }
    }

    private static void _HandleFolder(Track track) {
      var dirName = Path.GetDirectoryName(track.Path); //todo: dont safe path as string in track but as folder object
      var folder = Folder.AllFolders.FirstOrDefault(f => f.Directory.FullName == dirName);
      if (folder != null)
        folder.Tracks.Add(track);
    }

  }

}
