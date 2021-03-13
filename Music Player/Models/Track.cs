using Android.Media;
using MediaManager;
using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Models.DisplayGroup;
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;
using File = Java.IO.File;

[assembly: Dependency(typeof(Track))]
namespace Music_Player.Droid.Classes {
  public class Track {

    public string Path => this._file.Path;
    public string Title { get; private set; }

    public Artist[] Artists { get; set; }

    public string ArtistString => string.Join(" & ", this.Artists.Select(a => a.Name));

    public Genre[] Genres { get; set; }

    //todo: use separatorString
    public string GenreString => string.Join(" / ", this.Artists.Select(a => a.Name));

    public Album Album { get; set; }
    
    public TimeSpan Duration { get; }   

    public int Id { get; }

    public ImageSource CoverSource {
      get {

        //first time called code
        if (this.hasPicture == null) {
          var source = this._GetImageSource();
          if (source == null) {
            this.hasPicture = false;
            return ImageSource.FromFile(_DEFAULT_PIC_PATH);
          } else {
            this.hasPicture = true;
            return source;
          }
        }

        return this.hasPicture.Value ? this._GetImageSource() : ImageSource.FromFile(_DEFAULT_PIC_PATH);
      }
    }

    private bool? hasPicture;  
    private readonly File _file;
    private const string _DEFAULT_PIC_PATH = "record_new.png";

    //simple initializer
    //todo: put this into serialTrack instead?
    public Track(SerializableTrack serialTrack) {
      this._file = new File(serialTrack.Path);
      this.Title = serialTrack.Title;
      this.Duration = serialTrack.Duration;
      this.Id = this.Path.GetHashCode();
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
        return Color.FromHex("#8dd3c8");

      var nativeFeatures = DependencyService.Get<INativeFeatures>();
      return nativeFeatures.CalculateImageColor(this._GetBytes());
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
      return ((double)position.Ticks / duration.Ticks);
    }

    public override string ToString() => $"{this.ArtistString} - {this.Title}";
  }
}