using Music_Player.Enums;
using Music_Player.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GroupsView : ContentView {

    public GroupType GroupType {
      get => this.ViewModel.GroupType;
      set => this.ViewModel.GroupType = value;
    }

    public GroupsView() {
      this.InitializeComponent();
    }

    public void LvItemTapped(object sender, ItemTappedEventArgs e) {
      var lv = (ListView)sender;

      var group = (IDisplayGroup)lv.SelectedItem;
      this.Navigation.PushAsync(new GroupPage(group.Tracks, group.Name));
    }
  }
}