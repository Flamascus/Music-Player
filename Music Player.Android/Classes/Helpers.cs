using Android.Graphics;
using Color = Xamarin.Forms.Color;

namespace Music_Player.Droid.Classes {
  public static class Helpers {
    public static Color GetXamColor(this Bitmap @this, int x, int y) {
      var pixel = @this.GetColor(x, y);
      return new Color(
            pixel.Red(),
            pixel.Green(),
            pixel.Blue(),
            pixel.Alpha()
            );
    }
  }
}