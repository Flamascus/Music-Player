using MediaManager;
using Xamarin.Forms;

namespace Music_Player {
  public partial class App : Application {

    public App() {
        this.InitializeComponent();
        CrossMediaManager.Current.Init();
        //this.MainPage = new LoadingPage(this);
        this.MainPage = new AppShell();
    }

    protected override void OnStart() {

    }

    protected override void OnSleep() {
     // CacheManager.CacheQueue();
    }

    protected override void OnResume() {
      // CacheManager.TryReadQueueCache();
    }
  }
}