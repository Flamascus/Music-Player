using Music_Player.Models;
using Music_Player.Models.DisplayGroup;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Music_Player.ViewModels {
  public class FolderViewModel : ANotifyPropertyChanged {

    public Folder Folder { get; set; }
    public Folder[] Folders => this.Folder.SubFolders;
    public List<Track> Tracks => this.Folder.Tracks;

    public ICommand GoUpCommand { get; }

    public FolderViewModel() {
      while (Folder.IsLoading) //todo: solve better
        Task.Delay(50);

      this.Folder = Folder.Root;
      this.GoUpCommand = new Command(this.GoUp);
    }

    public void OnFolderTapped(string folderName) {
      var folder = this.Folders.First(f => f.Directory.Name == folderName);
      this.Folder = folder;
      this.OnPropertyChanged(nameof(this.Folders));
      this.OnPropertyChanged(nameof(this.Tracks));
    }

    public void GoUp() {
      if (this.Folder.Parent == null)
        return;

      this.Folder = this.Folder.Parent;
      this.OnPropertyChanged(nameof(this.Folders));
      this.OnPropertyChanged(nameof(this.Tracks));
    }

    //todo: copy paste from songsViewModel
    public void OnTrackTapped(Track track) {
      var trackQueue = TrackQueue.Instance;
      var queue = new List<Track>();
      var tracks = this.Tracks;
      var index = tracks.IndexOf(track);

      for (var i = index; i < tracks.Count; ++i)
        queue.Add(tracks[i]);

      trackQueue.ChangeQueue(queue);
      trackQueue.Play();
    }

  }
}
