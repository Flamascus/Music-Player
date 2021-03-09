using Music_Player.Interfaces;
using Music_Player.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Music_Player.Helpers {
  public static class Helpers {

    private static readonly Random _rnd = new Random();

    public static Stream GetStream(string path) {
      var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Helpers)).Assembly;
      return assembly.GetManifestResourceStream("Music_Player." + path);
    }

    /// <summary>
    /// use . instead of / for adressing folders (example: folder/file.txt => folder.file.txt)
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string[] ReadAllLines(string path) {
      var lines = new List<string>();
      string line;

      var stream = GetStream(path);
      using (var reader = new StreamReader(stream)) {
        while ((line = reader.ReadLine()) != null)
          lines.Add(line);
      }

      return lines.ToArray();
    }

    public static string ReadFile(string path) {
      using (var reader = new StreamReader(GetStream(path)))
        return reader.ReadToEnd();
    }

    public static void Write(string path, string content) {
      using var writer = new StreamWriter(GetStream(path));
      writer.Write(content);
    }

    public static void Shuffle<T>(this List<T> @this) {
      var i = @this.Count;

      while (i > 1) {
        i--;
        var k = _rnd.Next(i + 1);
        var temp = @this[k];
        @this[k] = @this[i];
        @this[i] = temp;
      }
    }

    public static T Dequeue<T>(this List<T> @this) {
      var item = @this.FirstOrDefault();
      @this.RemoveAt(0);
      return item;
    }

    public static List<ITrack> CreateTracklistFromPaths(string[] paths) {
      var tracks = new List<ITrack>();

      foreach (var path in paths) {
        var id = path.GetHashCode();
        var song = TrackList.Instance.FirstOrDefault(t => t.Id == id);
        if (song != null)
          tracks.Add(song);
      }

      return tracks;
    }

  }
}
