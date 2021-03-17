using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Music_Player.Views.CustomCells {
  public class ColorPickerCell : BetterCell {

    public static readonly BindableProperty ColorProperty
      = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPickerCell), null, propertyChanged: _OnColorChanged);

    public Color Color {
      get => (Color)this.GetValue(ColorProperty);
      set => this.SetValue(ColorProperty, value);
    }

    private readonly PancakeView _colorCircle;

    public ColorPickerCell() : base() {
      var colorCircle = new PancakeView() {
        CornerRadius = 40,
        WidthRequest = 35,
        HeightRequest = 40,
        Margin = new Thickness(5, 12),
      };
      this._colorCircle = colorCircle;
      this.controlDescriptionView.SetControl(colorCircle);
    }

    private static void _OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
      var @this = (ColorPickerCell)bindable;
      @this._colorCircle.BackgroundColor = (Color)newValue;
    }
  }
}
