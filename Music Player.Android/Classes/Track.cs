using Android.Graphics;
using Android.Media;
using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using Music_Player.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using File = Java.IO.File;

[assembly: Dependency(typeof(Track))]
namespace Music_Player.Droid.Classes {
  public class Track : ITrack {

    public const char _GENRE_SEPERATOR = '/';
    public string Title { get; private set; }
    public string CombinedArtistNames { get; private set; }
    public string[] ArtistNames { get; private set; }

    public ImageSource CoverSource {
      get {

        //first time called code
        if (hasPicture == null) {
          var source = _GetImageSource();
          if (source == null) {
            this.hasPicture = false;
            return ImageSource.FromFile("record.png");
          } else {
            this.hasPicture = true;
            return source;
          }
        }

        return this.hasPicture.Value ? _GetImageSource() : ImageSource.FromFile("record.png");
      }
    }

    public string Path => this._file.Path;

    public string[] GenreNames { get; } //todo: use genre class instead
    public string CombinedGenreName { get; }


    private bool? hasPicture;
    private readonly File _file;

    public Track() { }

    public ITrack Create(File file) => new Track(file);
    public ITrack Create(string path, string title, string combinedArtistNames, string combinedGenreNames)
      => new Track(path, title, combinedArtistNames, combinedGenreNames);

    public Track(string path, string title, string combinedArtistNames, string combinedgenreNames) {
      this._file = new File(path);
      this.Title = title;
      this.CombinedArtistNames = combinedArtistNames;
      this.ArtistNames = combinedArtistNames.Split('&').Select(a => a.Trim()).ToArray();   

      this.CombinedGenreName = combinedgenreNames;
      this.GenreNames = combinedgenreNames.Split('/');     
    }

    //todo: clean up
    private Track(File file) {
      this._file = file;

      var title = string.Empty;
      var combinedArtists = string.Empty;

      try {
        //try with taglib
        var fileTags = TagLib.File.Create(file.Path).Tag;
        var tGenres = fileTags.Genres;
        tGenres = tGenres.Length > 0 ? tGenres : new[] { string.Empty };
        var tArtists = fileTags.AlbumArtists;
        tArtists = tArtists.Length > 0 ? tGenres : new[] { string.Empty }; 

        var genreList = new List<string>();

        foreach(var g in tGenres) {
          var splitted = g.Split('/');
          foreach (var s in splitted)
            genreList.Add(s.Trim());
        }

        title = fileTags.Title;
        combinedArtists = fileTags.JoinedPerformers;        
        this.GenreNames = genreList.ToArray();
        this.ArtistNames = tArtists ?? new[] { string.Empty };

      } catch (Exception) {
        //try with medidataretriever
        try {
          var reader = new MediaMetadataRetriever();
          reader.SetDataSource(file.Path);

          title = reader.ExtractMetadata(MetadataKey.Title);
          combinedArtists = reader.ExtractMetadata(MetadataKey.Artist);

          var genre = reader.ExtractMetadata(MetadataKey.Genre); //todo: let no splitting still be an option
          if (genre == null)
            genre = string.Empty;
          var genres = genre.Split(_GENRE_SEPERATOR);

          for (var i = 0; i < genres.Length; ++i)
            genres[i] = Genre.TranslateId3Genre(genres[i].Trim());
          this.GenreNames = genres;

          this.ArtistNames = new[] { combinedArtists };

        } catch(Exception) {
          //set default values
          this.GenreNames = new[] { string.Empty }; //todo: check if null array also works;
          this.ArtistNames = new[] { string.Empty };
        }
      }

      this.Title = (title == null || title == string.Empty) ? file.Name : title.Trim();
      this.CombinedArtistNames = combinedArtists == null ? string.Empty : combinedArtists.Trim();
      this.CombinedGenreName = this._CreateCombinedGenreName(this.GenreNames);
    }

    //todo: shouldnt be done like this
    private string _CreateCombinedGenreName(string[] genres) {
      var combined = string.Empty;
      for (var i = 0; i < this.GenreNames.Length - 1; ++i)
        combined += this.GenreNames[i] + '/';

      return combined += this.GenreNames[this.GenreNames.Length - 1];
    }

    private ImageSource _GetImageSource() {
      var bytes = this._GetBytes();

      if (bytes == null) //todo: this is maybe not needed at all
        return null;

      return ImageSource.FromStream(() => new MemoryStream(bytes));
    }

    private byte[] _GetBytes() {
      var reader = new MediaMetadataRetriever();
      reader.SetDataSource(this._file.Path);
      return reader.GetEmbeddedPicture();
    }

    //todo: only calculate this one time
    public Color GetImageColor() {
      if (this.hasPicture == null || !this.hasPicture.Value)
        return Color.FromHex("#363636");

      var image = BitmapFactory.DecodeStream(new MemoryStream(_GetBytes()));
      var pixel = image.GetColor(0, 0);
      return new Color(
        pixel.Red(),
        pixel.Green(),
        pixel.Blue(),
        pixel.Alpha()
        );
    }
  }
}