using Music_Player.Droid.Classes;
using static Music_Player.Helpers.Helpers;

namespace Music_Player.Models {
  public class Genre : ADisplayGroup {

    public const char SEPARATOR = '/';

    private static string[] _id3Genres {
      get {
        if (__id3Genres == null)
          __id3Genres = ReadAllLines("Settings.id3genres.txt");

        return __id3Genres;
       }
      set => __id3Genres = value;
    }

    public static object DependencServices { get; private set; } //todo: check if this can be removed

    private static string[] __id3Genres;

    public Genre(string name, Track track) : base(name, track) { }

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