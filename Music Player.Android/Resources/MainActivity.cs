using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Music_Player.Droid.Classes;
using Music_Player.Services;
using System.IO;
using System.Diagnostics;
using Android.OS;
using System;
using Environment = System.Environment;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

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

      AppDomain.CurrentDomain.UnhandledException += _CurrentDomainOnUnhandledException;
      TaskScheduler.UnobservedTaskException += _TaskSchedulerOnUnobservedTaskException;


      base.OnCreate(savedInstanceState);

      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      this._DisplayCrashReport();
      this.LoadApplication(new App());
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    #region ErrorHandling
    private static void _TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs) {
      var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
      LogUnhandledException(newExc);
    }

    private static void _CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs) {
      var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
      LogUnhandledException(newExc);
    }

    internal static void LogUnhandledException(Exception exception) {
      try {
        if (Settings.Instance.SendReportsEnabled)
          Crashes.TrackError(exception);

        const string errorFileName = "Fatal.log";
        var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
        var errorFilePath = Path.Combine(libraryPath, errorFileName);
        var errorMessage = string.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
        DateTime.Now, exception.ToString());
        File.WriteAllText(errorFilePath, errorMessage);

        // Log to Android Device Logging.
        Android.Util.Log.Error("Crash Report", errorMessage);
      } catch {
        // just suppress any error logging exceptions
      }
    }

    /// <summary>
    // If there is an unhandled exception, the exception information is diplayed 
    // on screen the next time the app is started (only in debug configuration)
    /// </summary>
    [Conditional("DEBUG")]
    private void _DisplayCrashReport() {
      const string errorFilename = "Fatal.log";
      var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
      var errorFilePath = Path.Combine(libraryPath, errorFilename);

      if (!File.Exists(errorFilePath)) {
        return;
      }

      var errorText = File.ReadAllText(errorFilePath);
      new AlertDialog.Builder(this)
          .SetPositiveButton("Clear", (sender, args) => {
            File.Delete(errorFilePath);
          })
          .SetNegativeButton("Close", (sender, args) => {
            // User pressed Close.
          })
          .SetMessage(errorText)
          .SetTitle("Crash Report")
          .Show();
    }

    #endregion

  }
}