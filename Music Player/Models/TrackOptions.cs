using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Player.Enums;
using Music_Player.Interfaces;
using Music_Player.Models.Collections;

namespace Music_Player.Models {
  public static class TrackOptions {
    //todo: bidictionary
    public static Dictionary<TrackOption, string> OptionTexts = new Dictionary<TrackOption, string> {
      {TrackOption.PlayNext, "Play next"},
      {TrackOption.AddToQueue, "Add to queue" },
      {TrackOption.AddToEndOfQueue, "Add to end of queue"},
      {TrackOption.RemoveFromQueue, "Remove from queue"},
      {TrackOption.AddToPlaylist, "Add to playlist.." },
      {TrackOption.GoToArtist, "Go to artist"},
      {TrackOption.GoToAlbum, "Go to album"},
      {TrackOption.Details, "Details"}
    };

    public static void ExecuteOption(TrackOption option, ITrack track) {
      switch (option) {
        case TrackOption.PlayNext:
          TrackQueue.Instance.AddNext(track);
          break;

        case TrackOption.AddToQueue:
          TrackQueue.Instance.AddToQueue(track);
          break;

        case TrackOption.AddToEndOfQueue:
          TrackQueue.Instance.AddToEndOfQueue(track);
          break;

        case TrackOption.RemoveFromQueue:
          throw new NotImplementedException();

        case TrackOption.AddToPlaylist:
          PlaylistList.Instance.DisplayAddMenuAsync(track);
          break;

        case TrackOption.GoToArtist:
          throw new NotImplementedException();

        case TrackOption.GoToAlbum:
          throw new NotImplementedException();

        case TrackOption.Details:
          throw new NotImplementedException();

        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static async Task DisplayOptionsAsync(ITrack track, params TrackOption[] options) {
      var dic = OptionTexts;
      var cancelString = "Cancel";
      var texts = new string[options.Length];
      
      for (var i = 0; i < texts.Length; ++i)
        texts[i] = dic[options[i]];

      var selectText = await App.Current.MainPage.DisplayActionSheet(track.ToString(), cancelString, null, texts);

      if (selectText == null || selectText == cancelString)
        return;

      //todo: handle this with bidic instead
      ExecuteOption(dic.First(p => p.Value == selectText).Key, track);
    }

    public static async Task DisplayBasicOptionsAsync(ITrack track)
      => await DisplayOptionsAsync(track, new TrackOption[] {
      TrackOption.PlayNext, TrackOption.AddToQueue, TrackOption.AddToEndOfQueue, TrackOption.AddToPlaylist
      });

  }
}
