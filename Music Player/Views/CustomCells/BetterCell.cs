using Xamarin.Forms;

namespace Music_Player.Views.CustomCells {
  [ContentProperty("Control")]
  public class BetterCell : ViewCell {

    public static readonly BindableProperty TextProperty
      = BindableProperty.Create(nameof(Text), typeof(string), typeof(BetterCell), null, propertyChanged: _OnTextChanged);

    public static readonly BindableProperty DescriptionProperty
      = BindableProperty.Create(nameof(Description), typeof(string), typeof(BetterCell), null, propertyChanged: _OnDescriptionChanged);

    public static readonly BindableProperty ControlProperty
      = BindableProperty.Create(nameof(Control), typeof(object), typeof(BetterCell), propertyChanged: _OnControlChanged);

    public View Control {
      get => (View)this.GetValue(ControlProperty);
      set => this.SetValue(ControlProperty, value);
    }

    public string Text {
      get => (string)this.GetValue(TextProperty);
      set => this.SetValue(TextProperty, value);
    }

    public string Description {
      get => (string)this.GetValue(DescriptionProperty);
      set => this.SetValue(DescriptionProperty, value);
    }

    protected readonly ControlDescriptionView controlDescriptionView;

    public BetterCell() {
      this.controlDescriptionView = new ControlDescriptionView();
      this.View = this.controlDescriptionView;
    }

    private static void _OnTextChanged(BindableObject bindable, object oldValue, object newValue) {
      var view = (ControlDescriptionView)((BetterCell)bindable).View;
      view.SetText((string)newValue);
    }

    private static void _OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue) {
      var view = (ControlDescriptionView)((BetterCell)bindable).View;
      view.SetDescription((string)newValue);
    }

    private static void _OnControlChanged(BindableObject bindable, object oldValue, object newValue) {
      var view = (ControlDescriptionView)((BetterCell)bindable).View;
      view.SetControl((View)newValue, (View)oldValue);
    }
  }
}
