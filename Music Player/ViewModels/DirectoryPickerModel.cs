using System.IO;
using System.Linq;
using System.Windows.Input;
using Music_Player.Models;
using Music_Player.Services;
using Music_Player.Views.Pages;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
 
  public class DirectoryPickerModel : ANotifyPropertyChanged {

    private const string _INTERNAL_PATH = "/storage/emulated"; //todo: check if this path is the same on all phones

    public DirectoryInfo CurrentDirectory {
      get => this._currentDirectory;
      set {
        if (value.FullName == _INTERNAL_PATH) {
          Application.Current.MainPage.Navigation.PushAsync(new StoragePicker());
          return;
        }

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
      Application.Current.MainPage.Navigation.PopAsync();
    });

    public DirectoryPickerModel() {
      this.CurrentDirectory = new DirectoryInfo(Settings.Instance.MusicDirectory);
    }

    public DirectoryPickerModel(DirectoryInfo dir) {
      this.CurrentDirectory = dir;
    }

    //todo: check if try catch can be exchanged with null check
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
