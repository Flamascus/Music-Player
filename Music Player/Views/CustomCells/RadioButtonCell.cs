using Xamarin.Forms;

namespace Music_Player.Views.CustomCells {
  class RadioButtonCell : BetterCell {

    public static readonly BindableProperty IsCheckedProperty
      = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RadioButtonCell), null, propertyChanged: _OnIsCheckedProperty);

    public static readonly BindableProperty GroupNameProperty
      = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(RadioButtonCell), null, propertyChanged: _OnGroupNameProperty);


    public bool IsChecked {
      get => (bool)this.GetValue(IsCheckedProperty);
      set => this.SetValue(IsCheckedProperty, value); //todo: gets set on startup as well
    }

    public string GroupName {
      get => (string)this.GetValue(IsCheckedProperty);
      set => this.SetValue(IsCheckedProperty, value); //todo: gets set on startup as well
    }

    private readonly RadioButton _button;

    public RadioButtonCell() : base() {
      var button = new RadioButton();
      this._button = button;
      this.controlDescriptionView.SetControl(button);
    }

    private static void _OnIsCheckedProperty(BindableObject bindable, object oldValue, object newValue) {
      var @this = (RadioButtonCell)bindable;
      @this._button.IsChecked = (bool)newValue;
    }

    private static void _OnGroupNameProperty(BindableObject bindable, object oldValue, object newValue) {
      var @this = (RadioButtonCell)bindable;
      @this._button.GroupName = (string)newValue;
    }

  }
}
