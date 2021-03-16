using Music_Player.Enums;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ColorPickerView : ContentView {


    private const int _COLOR_AMOUNT = 8;
    private List<PancakeView> _colorViews = new List<PancakeView>();
    private readonly AppColor _appColor;
    private string _colorString;

    public ColorPickerView(AppColor appColor) {     
      this.InitializeComponent();
      this._appColor = appColor;
      var initialColor = appColor == AppColor.Primary ? "PrimaryInitial" : "AccentInitial";
      this._colorString = appColor == AppColor.Primary ? "Primary" : "Accent";

      var startColor = (Color)App.Current.Resources[initialColor];
      var stepSize = (double)1 / _COLOR_AMOUNT;

      //create main colors
      for (var i = 0; i < _COLOR_AMOUNT; ++i) {
        var hueValue = startColor.Hue + (stepSize * i);
        if (hueValue > 1)
          --hueValue;

        this._AddColorToFlex(this.mainColorsFlex, startColor.WithHue(hueValue));
      }
      this._ChangeShadeColors(startColor);
    }

    private void _ChangeShadeColors(Color mainColor) {
      var shadeFlex = this.ColorShadesFlex;
      shadeFlex.Children.Clear();
      var saturation = mainColor.Saturation;
      var luminosity = mainColor.Luminosity;

      this._AddColorToFlex(shadeFlex, mainColor.WithSaturation(luminosity * 0.3), false);
      this._AddColorToFlex(shadeFlex, mainColor.WithSaturation(saturation * 0.3), false);
      this._AddColorToFlex(shadeFlex, mainColor.WithSaturation(saturation * 0.6), false);
      this._AddColorToFlex(shadeFlex, mainColor.WithLuminosity(luminosity * 0.6), false);
    }

    private void _AddColorToFlex(FlexLayout flex, Color color, bool isMainColor = true) {
      var pancakeView = new PancakeView {
        WidthRequest = 50,
        HeightRequest = 50,
        CornerRadius = 50,
        Margin = 7,
        BackgroundColor = color
      };

      var gestureRecognizer = new TapGestureRecognizer();

      if (isMainColor) {
        gestureRecognizer.Tapped += this._ColorTapped;
        gestureRecognizer.Tapped += (sender, _) => this._ChangeShadeColors(((PancakeView)sender).BackgroundColor);
      } else
        gestureRecognizer.Tapped += this._ColorTapped;

      pancakeView.GestureRecognizers.Add(gestureRecognizer);

      flex.Children.Add(pancakeView);
    }

    //todo: dont actually set color here, just return chosen color in the end!
    private void _ColorTapped(object sender, EventArgs e) {
      var pancakeView = (PancakeView)sender;
      this._colorViews.ForEach(p => p.Border = null);

      pancakeView.Border = this.colorPancakeViewTemplate.Border;
      var color = pancakeView.BackgroundColor;
      App.Current.Resources[this._colorString] = color;
      var deltaLuminosity = 1 - color.Luminosity;

      if (this._appColor == AppColor.Primary)
        App.Current.Resources["PrimaryBright"] = color.WithLuminosity(color.Luminosity + (deltaLuminosity * 0.5));
      Helpers.Helpers.SetBarColorDefaults();
    }

  }
}