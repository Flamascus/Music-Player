using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.CustomCells {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ControlDescriptionView : ContentView {

    /// <summary>
    /// Provides text and description for a view
    /// </summary>
    public ControlDescriptionView() {
      this.InitializeComponent();
    }

    public void SetText(string text) => this.lblText.Text = text;
    public void SetDescription(string text) {
      if (text == string.Empty) {
        this.lblDescription.IsVisible = false;
        this.HeightRequest = 45;
      } else {
        this.lblDescription.IsVisible = true;
        this.HeightRequest = 60;
      }

      this.lblDescription.Text = text;        
    }

    public void SetControl(View newControl, View oldControl = null) {
      if (oldControl != null)
        this.Grid.Children.Remove(oldControl);

      this.Grid.Children.Add(newControl, 1, 0);
      Grid.SetRowSpan(newControl, 2); //todo: might need to be swapped with line underneath
    }

  }

}