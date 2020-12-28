using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Music_Player.Droid.Classes;

namespace Music_Player.Droid {
  [Activity(Label = "Music Player", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
    protected override void OnCreate(Bundle savedInstanceState) {
      NativeFeatures.MainActivity = this;

      if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
        //Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 67, 138));
        this.Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 79, 163));
        this.Window.SetNavigationBarColor(Android.Graphics.Color.Black);
      }

      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(savedInstanceState);

      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      LoadApplication(new App());
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

  }
}