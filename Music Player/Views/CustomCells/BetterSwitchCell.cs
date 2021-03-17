using Xamarin.Forms;

namespace Music_Player.Views.CustomCells {
  public class BetterSwitchCell : BetterCell {

    public static readonly BindableProperty IsToggledProperty
     = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(BetterSwitchCell), null, propertyChanged: _OnIsToggledProperty);

    public bool IsToggled {
      get => (bool)this.GetValue(IsToggledProperty);
      set => this.SetValue(IsToggledProperty, value); //todo: gets set on startup as well
    }

    private readonly Switch _switch;

    public BetterSwitchCell() : base() {
      var @switch = new Switch();
      @switch.SetDynamicResource(Switch.OnColorProperty, "Primary");
      @switch.SetDynamicResource(Switch.ThumbColorProperty, "PrimaryBright");
      this._switch = @switch;

      this.controlDescriptionView.SetControl(@switch);
    }

    private static void _OnIsToggledProperty(BindableObject bindable, object oldValue, object newValue) {
      var @this = (BetterSwitchCell)bindable;
      @this._switch.IsToggled = (bool)newValue;
    }
  }
}
