using Music_Player.Models.DisplayGroup;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class FolderView : ContentView {
    public FolderView() {
      while (Folder.IsLoading)
        Task.Delay(50);

      this.InitializeComponent();
    }
  }
}