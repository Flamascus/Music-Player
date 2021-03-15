using Syncfusion.DataSource.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ColorPickerView : ContentView {

    public event EventHandler<EventArgs> ColorSelected;
    private const int _COLOR_AMOUNT = 8;
    private PancakeView[] _colorViews = new PancakeView[_COLOR_AMOUNT];

    public ColorPickerView() {
      this.InitializeComponent();
      var startColor = this.colorControlTemplate.BackgroundColor;
      var flexLayout = this.FlexLayout;
      var stepSize = (double)1 / _COLOR_AMOUNT;
      
      for (var i = 0; i < _COLOR_AMOUNT; ++i) {
        var hueValue = startColor.Hue + (stepSize * i);
        if (hueValue > 1)
          --hueValue;

        var color = startColor.WithHue(hueValue);

        var pancakeView = new PancakeView {
          WidthRequest = 50,
          HeightRequest = 50,
          CornerRadius = 50,
          Margin = 7,
          BackgroundColor = color,
        };

        var gestureRecognizer = new TapGestureRecognizer();
        gestureRecognizer.Tapped += this._GestureRecognizer_Tapped;
        pancakeView.GestureRecognizers.Add(gestureRecognizer);

        this._colorViews[i] = pancakeView;
        flexLayout.Children.Add(pancakeView);
      }
    }

    private void _GestureRecognizer_Tapped(object sender, EventArgs e) {
      this._colorViews.ForEach(p => p.Border = null);

      var pancakeView = (PancakeView)sender;
      pancakeView.Border = new Border() { Thickness = 1, Color = Color.White };
      var color = pancakeView.BackgroundColor;
      App.Current.Resources["Primary"] = color;
      Helpers.Helpers.SetBarColorDefaults();
      this.ColorSelected?.Invoke(this, new EventArgs());
    }
  }
}