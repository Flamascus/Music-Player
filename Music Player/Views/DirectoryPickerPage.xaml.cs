using Music_Player.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Music_Player.Services;

namespace Music_Player.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]

  //todo: make into more general folder selector
  public partial class DirectoryPickerPage : ContentPage {

    private readonly DirectoryPickerModel _model;

    public event EventHandler<EventArgs> DirectorySelected;

    public DirectoryPickerPage() {
      var model = new DirectoryPickerModel();
      this._model = model;
      this.BindingContext = model;
      this.InitializeComponent();
    }

    private void _ListView_ItemTapped(object sender, ItemTappedEventArgs e) {
      this._model.GoToChild(e.ItemIndex);
    }

    private void SelectThisDirectory(object sender, EventArgs e) {
      Settings.Instance.MusicDirectory = this._model.CurrentDirectory.FullName;
      this.DirectorySelected?.Invoke(this, new EventArgs());
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}