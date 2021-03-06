using System;
using Java.IO;
using Xamarin.Forms;

namespace Music_Player.Interfaces {
  public interface ITrack {
    string Title { get; }
    string CombinedArtistNames { get; }
    string[] ArtistNames { get; }
    ImageSource CoverSource { get; }
    string Path { get; }
    string[] GenreNames { get; }
    string CombinedGenreName { get; }
    TimeSpan Duration { get; }

    ITrack Create(File file);
    ITrack Create(string path, string title, string combinedArtistNames, string combinedGenreNames, TimeSpan duration);
    Color GetImageColor();
    double GetProgress();
    void JumpToPercent(double value);
  }
}
