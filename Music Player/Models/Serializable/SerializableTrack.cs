using System;
using System.Linq;

namespace Music_Player.Models {
  public class SerializableTrack {

    public string Path { get; set; }
    public string Title { get; set; }
    public string[] Artists { get; set; }
    public string[] Genres { get; set; }

    public string Album { get; set; }
    public TimeSpan Duration { get; set; }

    public static SerializableTrack FromTrack(Track track) => new SerializableTrack() {
      Path = track.Path,
      Title = track.Title,
      Artists = track.Artists.Select(a => a.Name).ToArray(),
      Genres = track.Genres.Select(g => g.Name).ToArray(),
      Album = track.Album.Name,
      Duration = track.Duration
    };

    public override string ToString() => this.Path;

  }
}