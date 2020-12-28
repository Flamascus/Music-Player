using Music_Player.Interfaces;
using Music_Player.Services;
using System.Collections.Generic;
using System.Reflection;

namespace Music_Player.Models {
  public class Genre {
    public string GenreName { get; }
    public int TrackAmount => this.Tracks.Count;
    public string TrackAmountUi => this.TrackAmount + " Songs";
    public List<ITrack> Tracks { get; }

    private static string[] _id3Genres {
      get {
        if (__id3Genres == null)
          __id3Genres = Helpers.ReadAllLines("Settings.genres.txt");

        return __id3Genres;
       }
      set => __id3Genres = value;
    }

    public static object DependencServices { get; private set; }

    private static string[] __id3Genres;

    public Genre(string name, List<ITrack> tracks) {
      this.GenreName = name;
      this.Tracks = tracks;
    }

    public static string TranslateId3Genre(string name) {
      if (name.StartsWith("(") && name.EndsWith(")")) {
        name = name.Remove(0, 1);
        name = name.Remove(name.Length - 1, 1);
      }

        if (!int.TryParse(name, out var number))
        return name;

        //return name; //todo: reading file doesnt work atm

      if (number >= _id3Genres.Length)
        return name;
      
      return _id3Genres[number];
    }

  }
}