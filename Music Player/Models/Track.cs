using MediaManager;
using Music_Player.Models;
using Music_Player.Models.DisplayGroup;
using System;
using System.Linq;
using Xamarin.Forms;
using File = Java.IO.File;

[assembly: Dependency(typeof(Track))]
namespace Music_Player.Models {
  public class Track {

    public string Path => this._file.Path;
    public string Title { get; private set; }

    public Artist[] Artists { get; set; }

    public string ArtistString => string.Join(Artist.SEPARATOR, this.Artists.Select(a => a.Name));

    public Genre[] Genres { get; set; }

    //todo: use separatorString, cache
    public string GenreString => string.Join(Genre.SEPARATOR, this.Artists.Select(a => a.Name));

    public Album Album { get; set; }
    public TimeSpan Duration { get; }
    public int Id { get; }
    public Cover Cover { get; }


    private readonly File _file;


    //todo: put this into serialTrack instead?
    public Track(SerializableTrack serialTrack) {
      this._file = new File(serialTrack.Path);
      this.Cover = new Cover(this._file);
      this.Title = serialTrack.Title;
      this.Duration = serialTrack.Duration;
      this.Id = this.Path.GetHashCode();
    }

    public void JumpToPercent(double value) {
      var duration = this.Duration;
      var position = TimeSpan.FromTicks((long)(duration.Ticks * value));
      CrossMediaManager.Current.SeekTo(position);
    }

    public TimeSpan GetProgress() => CrossMediaManager.Current.Position;
    public void SetProgress(TimeSpan progress) => CrossMediaManager.Current.SeekTo(progress);

    public double GetProgressPercent() {
      var duration = this.Duration;
      var position = CrossMediaManager.Current.Position;
      return (double)position.Ticks / duration.Ticks;
    }

    public override string ToString() => $"{this.ArtistString} - {this.Title}";
  }
}