using Music_Player.Interfaces;
using System;
using Xamarin.Forms;

namespace Music_Player.Models {
  public class SerializableTrack {

    public string Path { get; }
    public string Title { get; }
    public string CombinedArtistNames { get; }
    public string CombinedGenreNames { get; }
    public string Album { get; }

    public TimeSpan Duration { get; }

    public SerializableTrack(string path, string title, string combinedArtistNames, string combinedGenreNames, string album, TimeSpan duration) {
      this.Path = path;
      this.Title = title;
      this.CombinedArtistNames = combinedArtistNames;
      this.CombinedGenreNames = combinedGenreNames;
      this.Duration = duration;
      this.Album = album;
    }

    public static SerializableTrack FromTrack(ITrack track)
      => new SerializableTrack(track.Path, track.Title, track.CombinedArtistNames, track.CombinedGenreName, track.Album, track.Duration);

    public ITrack ToTrack() {
      var builder = DependencyService.Get<ITrack>();
      return builder.Create(this.Path, this.Title, this.CombinedArtistNames, this.CombinedGenreNames, this.Album, this.Duration);
    }
  }
}
