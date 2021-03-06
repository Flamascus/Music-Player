using MediaManager;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Music_Player {
  public partial class App : Application {

    public App() {
      try {
        this.InitializeComponent();
        CrossMediaManager.Current.Init();
        //this.MainPage = new LoadingPage(this);
        this.MainPage = new AppShell();
      } catch (Exception e) {
        Trace.WriteLine(e.Message);
      }
    }

    protected override void OnStart() {

    }

    protected override void OnSleep() {
    }

    protected override void OnResume() {
    }
  }
}