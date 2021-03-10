using MediaManager;
using Music_Player.Services;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace Music_Player {
  public partial class App : Application {

    public App() {
      this.InitializeComponent();
      CrossMediaManager.Current.Init();
      this.MainPage = new AppShell();
    }

    protected override void OnStart() {
      if (Settings.Instance.SendReportsEnabled) {
        AppCenter.Start("android=b02bf2d0-1291-443c-9f73-9d4a186e7e19;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));        
      }
      Analytics.TrackEvent("App started!");    
    }

    protected override void OnSleep() {
      CacheManager.CacheQueue();
      Analytics.TrackEvent("Sleeping!");
    }

    protected override void OnResume() {
      Analytics.TrackEvent("Sleeping!");
    }

  }
}