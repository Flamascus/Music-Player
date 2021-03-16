using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Music_Player.Views.UserControls {
  [XamlCompilation(XamlCompilationOptions.Compile)]

  //todo: almost same code as betterswitchcellview, try to union both into one general control!
  public partial class ColorPickerViewCell : ContentView {

    public static readonly BindableProperty TextProperty
      = BindableProperty.Create(nameof(Text), typeof(string), typeof(ColorPickerViewCell), null, propertyChanged: _OnTextChanged);

    public static readonly BindableProperty DescriptionProperty
      = BindableProperty.Create(nameof(Description), typeof(string), typeof(ColorPickerViewCell), null, propertyChanged: _OnDescriptionChanged);

    public static readonly BindableProperty ColorProperty
      = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPickerViewCell), null, propertyChanged: _OnColorChanged);
    

    public string Text {
      get => (string)this.GetValue(TextProperty);
      set => this.SetValue(TextProperty, value);
    }

    public string Description {
      get => (string)this.GetValue(DescriptionProperty);
      set => this.SetValue(DescriptionProperty, value);
    }

    public Color Color {
      get => (Color)this.GetValue(ColorProperty);
      set => this.SetValue(ColorProperty, value);
    }

    public ColorPickerViewCell() => this.InitializeComponent();

    private static void _OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (ColorPickerViewCell)bindable;
      control.lblText.Text = (string)newValue;
    }

    private static void _OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (ColorPickerViewCell)bindable;
      control.lblDescription.Text = (string)newValue;
    }

    private static void _OnColorChanged(BindableObject bindable, object oldValue, object newValue) {
      var control = (ColorPickerViewCell)bindable;
      control.pancakeView.BackgroundColor = (Color)newValue;
    }
  }
}