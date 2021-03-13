using Music_Player.Droid.Classes;
using Music_Player.Enums;
using Music_Player.Helpers;
using Music_Player.Models.DisplayGroup;
using Music_Player.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Player.Models.Collections {

  //todo: dont use readonly here
  public class PlaylistList : LoadableList<Playlist> {

    public static PlaylistList Instance = new PlaylistList();
    private PlaylistList() { }

    public void Init() {
      CacheManager.TryReadPlaylistCache(out var playlists);
      this.items = playlists;
      this.IsLoading = false;
    }

    //todo: not sure if this actually belongs here
    public async void DisplayAddMenuAsync(Track track) {
      var cancelString = "Cancel";
      var createPlaylistString = "New playlist..";
      var optionStrings = new List<string> { createPlaylistString };
      optionStrings.AddRange(this.items.Select(p => p.Name));

      var selectedText = await App.Current.MainPage.DisplayActionSheet(
        TrackOptions.OptionTexts[TrackOption.AddToPlaylist], cancelString, null, optionStrings.ToArray());

      if (selectedText == null || selectedText == cancelString)
        return;

      if (selectedText == createPlaylistString)
        await this._CreatePlaylistAsync(track);
      else
        this.items.FirstOrDefault(p => p.Name == selectedText).Tracks.Add(track);

      CacheManager.CachePlaylists();
    }

    private async Task _CreatePlaylistAsync(Track track) {
      var name = await App.Current.MainPage.DisplayPromptAsync("New playlist", "Enter playlist name:");
      if (name == null || name == "Cancel")
        return;

      var items = this.items;
      var newItems = new List<Playlist> { new Playlist(name, new List<Track> { track })};
      newItems.AddRange(items);
      this.items = newItems;
    }

  }
}
