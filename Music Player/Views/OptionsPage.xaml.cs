using System;
using Music_Player.Interfaces;
using Music_Player.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class OptionsPage : ContentPage {

    private readonly OptionsViewModel _model;

    public OptionsPage(ITrack track) {
      this.Title = track.Title;
      this._model = new OptionsViewModel(track);
      this.BindingContext = this._model;
      this.InitializeComponent();
    }

    private void AddNext(object sender, EventArgs e) {
      this._model.AddNext();
      this.Navigation.PopAsync();
    }

    private void AddToQueue(object sender, EventArgs e) {
      this._model.AddToQueue();
      this.Navigation.PopAsync();
    }
  }
}