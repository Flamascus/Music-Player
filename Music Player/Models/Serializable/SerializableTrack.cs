using Music_Player.Interfaces;
using System;
using Xamarin.Forms;

namespace Music_Player.Models {
  public class SerializableTrack {

    public string Path { get; }
    public string Title { get; }
    public string CombinedArtistNames { get; }
    public string CombinedGenreNames { get; }
    public TimeSpan Duration { get; }

    public SerializableTrack(string path, string title, string combinedArtistNames, string combinedGenreNames, TimeSpan duration) {
      this.Path = path;
      this.Title = title;
      this.CombinedArtistNames = combinedArtistNames;
      this.CombinedGenreNames = combinedGenreNames;
      this.Duration = duration;
    }

    public static SerializableTrack FromTrack(ITrack track)
      => new SerializableTrack(track.Path, track.Title, track.CombinedArtistNames, track.CombinedGenreName, track.Duration);

    public ITrack ToTrack() {
      var builder = DependencyService.Get<ITrack>();
      return builder.Create(this.Path, this.Title, this.CombinedArtistNames, this.CombinedGenreNames, this.Duration);
    }
  }
}
