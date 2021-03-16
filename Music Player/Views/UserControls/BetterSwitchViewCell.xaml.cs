using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BetterSwitchCellView : ContentView {

    public static readonly BindableProperty TextProperty
      = BindableProperty.Create(nameof(Text), typeof(string), typeof(BetterSwitchCellView), null, propertyChanged: _OnTextChanged);

    public static readonly BindableProperty DescriptionProperty
      = BindableProperty.Create(nameof(Description), typeof(string), typeof(BetterSwitchCellView), null, propertyChanged: _OnDescriptionChanged);

    public static readonly BindableProperty IsToggledProperty
     = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(BetterSwitchCellView), null, propertyChanged: _OnIsToggledProperty);

    public string Text {
      get => (string)this.GetValue(TextProperty);
      set => this.SetValue(TextProperty, value);
    }

    public string Description {
      get => (string)this.GetValue(DescriptionProperty);
      set => this.SetValue(DescriptionProperty, value);
    }

    public bool IsToggled {
      get => (bool)this.GetValue(IsToggledProperty);
      set => this.SetValue(IsToggledProperty, value); //todo: 
    }

    public event EventHandler<ToggledEventArgs> Toggled;
    private readonly Color _deactivatedThumbColor;

    public BetterSwitchCellView() {
      this.InitializeComponent();
      this._deactivatedThumbColor = this.@switch.ThumbColor;
      this.@switch.Toggled += (sender, args) => this.IsToggled = args.Value;
    }

    private static void _OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (BetterSwitchCellView)bindable;
      control.lblText.Text = (string)newValue;
    }

    private static void _OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (BetterSwitchCellView)bindable;
      control.lblDescription.Text = (string)newValue;
    }

    private static void _OnIsToggledProperty(BindableObject bindable, object oldValue, object newValue) {
      var control = (BetterSwitchCellView)bindable;
      control.@switch.IsToggled = (bool)newValue;
    }

    private void _Switch_Toggled(object _, ToggledEventArgs e) => this.Toggled?.Invoke(this, e);
  }
}