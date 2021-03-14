using System.IO;
using System.Linq;
using System.Windows.Input;
using Music_Player.Services;
using Xamarin.Forms;

namespace Music_Player.Models {
 
  public class DirectoryPickerModel : ANotifyPropertyChanged {

    public DirectoryInfo CurrentDirectory {
      get => this._currentDirectory;
      private set {
        if (value.FullName == "/storage/emulated") //todo: probably only works for my phone
          return;

        this.Children = value.GetDirectories()
          .Select(f => new SystemFile(f))
          .Union(value.GetFiles()
          .Where(f => FileReader.SupportedFormats
          .Any(s => f.Name
          .EndsWith(s)))
          .Select(f => new SystemFile(f)))
          .ToArray();

        this._currentDirectory = value;
        this.OnPropertyChanged(nameof(this.Children));
        this.OnPropertyChanged(nameof(this.GoHigherVisible));
        this.OnPropertyChanged(nameof(this.FullName));
        this.OnPropertyChanged(nameof(this.Name));
      }
    }

    public string Name => this.CurrentDirectory.Name;
    public string FullName => string.Join(" > ",this.CurrentDirectory.FullName.Split('/'));

    private DirectoryInfo _currentDirectory;

    public SystemFile[] Children { get; private set; }

    public bool GoHigherVisible => this._CanGoHigher();

    public ICommand GoUpCommand => new Command(() => this.CurrentDirectory = this.CurrentDirectory.Parent);
    public ICommand SelectThisCommand => new Command(() => {
      Settings.Instance.MusicDirectory = this._currentDirectory.ToString();
      App.Current.MainPage.Navigation.PopAsync();
    });

    public DirectoryPickerModel() {
      this.CurrentDirectory = new DirectoryInfo(Settings.Instance.MusicDirectory);
    }

    private bool _CanGoHigher() {
      var parent = this._currentDirectory.Parent;
      try {
        parent.GetFileSystemInfos();
        return true;
      } catch {
        return false;
      }
    }

    public void GoToChild(int index) {
      if (this.Children[index].Info is DirectoryInfo dInfo)
        this.CurrentDirectory = dInfo;
    }

  }
}
