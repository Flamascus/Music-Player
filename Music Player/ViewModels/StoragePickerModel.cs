using System.Linq;
using System.Text.RegularExpressions;
using Java.IO;
using Music_Player.Interfaces;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
  public class StoragePickerModel {
    public string[] StorageNames { get; }
    public string[] StoragePaths { get; }

    private readonly File[] _StorageFiles = DependencyService.Get<INativeFeatures>().ListAllStorageDevices();

    private readonly Regex _pathRegex = new Regex(@"^([\/]+[^\/]*){3}"); //returns the highest folder on the storage path

    public StoragePickerModel() {
      this.StorageNames = this._StorageFiles.Select(f => f.AbsolutePath.Split('/')[2]).ToArray();
      this.StoragePaths = this._StorageFiles.Select(f => this._pathRegex.Match(f.AbsolutePath).ToString()).ToArray();
    }
  }
}
