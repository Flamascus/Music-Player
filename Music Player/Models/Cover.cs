using Android.Media;
using Music_Player.Interfaces;
using System.IO;
using Xamarin.Forms;
using File = Java.IO.File;

namespace Music_Player.Models {

  /// <summary>
  /// The coverImage of a track
  /// </summary>
  public class Cover {

    private const string _DEFAULT_PIC_PATH = "record_new.png";

    public ImageSource Source {
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
    private Color? _dominantColor;

    public Cover(File file) {
      this._file = file;
    }

    public Color GetDominantColor() {
      var color = this._dominantColor;
      if (color.HasValue)
        return color.Value;

      color = this.hasPicture == null || !this.hasPicture.Value
        ? Color.FromHex("#8dd3c8")
        : DependencyService.Get<INativeFeatures>().CalculateImageColor(this._GetBytes());

      this._dominantColor = color;
      return color.Value;
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

  }
}
