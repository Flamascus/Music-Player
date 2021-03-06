using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Music_Player.Services {
  public class ANotifyPropertyChanged : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
