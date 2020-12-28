using Music_Player.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoadingPage : ContentPage {

    public LoadingPage() {
      var model = new LoadingViewModel();
      model.ProgressFinished += Model_ProgressFinished;
      this.BindingContext = model;
      InitializeComponent();
    }

    private void Model_ProgressFinished(object sender, EventArgs e) {
      this.TestLabel.Text = "Finished!";
      this.Navigation.PopAsync();
    }
  }
}