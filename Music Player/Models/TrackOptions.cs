using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Player.Droid.Classes;
using Music_Player.Enums;
using Music_Player.Models.Collections;
using Music_Player.Views;

namespace Music_Player.Models {
  public static class TrackOptions {
    //todo: bidictionary
    public static readonly IReadOnlyDictionary<TrackOption, string> OptionTexts = new Dictionary<TrackOption, string> {
      {TrackOption.PlayNext, "Play next"},
      {TrackOption.AddToQueue, "Add to queue" },
      {TrackOption.AddToEndOfQueue, "Add to end of queue"},
      {TrackOption.RemoveFromQueue, "Remove from queue"},
      {TrackOption.AddToPlaylist, "Add to playlist" },
      {TrackOption.RemoveFromPlaylist, "Remove from playlist" },
      {TrackOption.GoToArtist, "Go to artist"},
      {TrackOption.GoToAlbum, "Go to album"},
      {TrackOption.Details, "Details"}
    };

    /// <summary>
    /// The options that are always visible
    /// </summary>
    public static readonly TrackOption[] BasicOptions = new TrackOption[] {
      TrackOption.PlayNext,
      TrackOption.AddToQueue,
      TrackOption.AddToEndOfQueue,
      TrackOption.AddToPlaylist,
      TrackOption.GoToArtist
    };

    private static void _ExecuteOption(TrackOption option, Track track) {
      var queue = TrackQueue.Instance;
      var playlist = PlaylistList.Instance;

      switch (option) {
        case TrackOption.PlayNext:
          queue.AddNext(track);
          break;

        case TrackOption.AddToQueue:
          queue.AddToQueue(track);
          break;

        case TrackOption.AddToEndOfQueue:
          queue.AddToEndOfQueue(track);
          break;

        case TrackOption.RemoveFromQueue:
          queue.Remove(track);
          break;

        case TrackOption.AddToPlaylist:
          playlist.DisplayAddMenuAsync(track);
          break;

        case TrackOption.RemoveFromPlaylist:
          throw new NotImplementedException();

        case TrackOption.GoToArtist: //todo: error handling when no artist available
          var artist = track.Artists.FirstOrDefault();
          App.Current.MainPage.Navigation.PushAsync(new GroupPage(artist.Tracks, artist.Name));
          break;

        case TrackOption.GoToAlbum:
          throw new NotImplementedException();

        case TrackOption.Details:
          throw new NotImplementedException();

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static async Task _DisplayOptionsAsync(Track track, params TrackOption[] options) {
      var dic = OptionTexts;
      var cancelString = "Cancel";
      var texts = new string[options.Length];

      for (var i = 0; i < texts.Length; ++i)
        texts[i] = dic[options[i]];

      var selectText = await App.Current.MainPage.DisplayActionSheet(track.ToString(), cancelString, null, texts);

      if (selectText == null || selectText == cancelString)
        return;

      //todo: handle this with bidic instead
      _ExecuteOption(dic.First(p => p.Value == selectText).Key, track);
    }

    public static async Task DisplayBasicOptionsAsync(Track track) 
      => await _DisplayOptionsAsync(track, BasicOptions);

    /// <summary>
    /// Displays basic options and additional extra options
    /// </summary>
    /// <param name="track">the track to apply this option on</param>
    /// <param name="options">the additional options</param>
    /// <returns></returns>
    public static async Task DisplaySpecialOptionsAsync(Track track, params TrackOption[] options) {
      var newArray = Helpers.Helpers.MergeArrays(BasicOptions, options);
      await _DisplayOptionsAsync(track, newArray);
    }

  }
}
