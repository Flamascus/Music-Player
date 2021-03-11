using System;
using Java.IO;
using Xamarin.Forms;

namespace Music_Player.Interfaces {
  public interface ITrack  {
    string Title { get; }
    string CombinedArtistNames { get; }
    string[] ArtistNames { get; }
    ImageSource CoverSource { get; }
    string Path { get; }
    string[] GenreNames { get; }
    string CombinedGenreName { get; }
    TimeSpan Duration { get; }
    int Id { get; }
    string Album { get; }

    ITrack Create(File file);
    ITrack Create(string path, string title, string combinedArtistNames, string combinedGenreNames, string album, TimeSpan duration);
    Color GetImageColor();
    TimeSpan GetProgress();
    double GetProgressPercent();
    void JumpToPercent(double value);
    void SetProgress(TimeSpan progress);
  }
}
