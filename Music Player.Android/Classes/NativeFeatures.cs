using Android;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Music_Player.Droid.Classes;
using Music_Player.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NativeFeatures))]
namespace Music_Player.Droid.Classes {
  public class NativeFeatures : INativeFeatures {

    private static string _externalPath => Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath;
    private static string _appPath => MainActivity.ApplicationContext.GetExternalFilesDir(null).AbsolutePath;

    private static string _GetDirectoryString(bool useInternalPath) => useInternalPath ? _appPath : _externalPath;


    public static MainActivity MainActivity { get; set; }

    public void RequestPerimissions() {
      if (Build.VERSION.SdkInt >= BuildVersionCodes.M) {
        if (!(this._CheckPermissionGranted(Manifest.Permission.ReadExternalStorage)
          && !this._CheckPermissionGranted(Manifest.Permission.WriteExternalStorage)))
          this._RequestPermission();
      }

      while (!this._CheckPermissionGranted(Manifest.Permission.WriteExternalStorage)
        || !this._CheckPermissionGranted(Manifest.Permission.ReadExternalStorage))
        Task.Delay(50);
    }

    private void _RequestPermission() {
      ActivityCompat.RequestPermissions(MainActivity, new string[] {
        Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
    }

    // Check if the permission is already available.
    private bool _CheckPermissionGranted(string Permissions)
      => ContextCompat.CheckSelfPermission(MainActivity, Permissions) == Permission.Granted;


    public string[] ReadAllLines(string path) => File.ReadAllLines(path); 

    public string MusicLibaryPath
      => Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();


    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
      => Directory.EnumerateFiles(path, searchPattern, searchOption);

    public IEnumerable<FileInfo> EnumerateFiles2(string path) {
      var directory = new DirectoryInfo(path);
      return directory.EnumerateFiles();
    }

    public IEnumerable<Java.IO.File> EnumerateFiles3(string path) {
      var filePaths = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
      return filePaths.Select(f => new Java.IO.File(f));
    }

    public void WriteAppFile(string fileName, string content, bool useInternalPath = true) {

      var path = Path.Combine(_GetDirectoryString(useInternalPath), fileName);

      using var stream = File.Create(path);
      using var writer = new StreamWriter(stream);
      writer.Write(content);
    }

    //todo: can probably put this in mainlogic
    public string[] ReadAllLinesAppFile(string fileName, bool useInternalPath = true) {
      var path = Path.Combine(_GetDirectoryString(useInternalPath), fileName);
      return File.Exists(path) ? File.ReadAllLines(path) : new string[0];
    }

    public string ReadAppFile(string fileName, bool useInternalPath = true) {
      var path = Path.Combine(_GetDirectoryString(useInternalPath), fileName);

      return File.ReadAllText(path);

      //using var stream = File.Create(path);
      //using var reader = new StreamReader(stream);
      //return reader.ReadToEnd();
    }

    public void SetStatusBarColor(Color color) {
      MainActivity.Window.SetStatusBarColor(_ToAndroidColor(color));
    }

    public void SetNavigationBarColor(Color color) {
      MainActivity.Window.SetNavigationBarColor(_ToAndroidColor(color));
    }

    public void SetFullScreen() {
      MainActivity.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
    }

    private static Android.Graphics.Color _ToAndroidColor(Color color) {
      return Android.Graphics.Color.Argb(
        (int)(255 * color.A),
        (int)(255 * color.R),
        (int)(255 * color.G),
        (int)(255 * color.B)
        );
    }

    public bool DirectoryExists(string path) => Directory.Exists(path);

  }
}