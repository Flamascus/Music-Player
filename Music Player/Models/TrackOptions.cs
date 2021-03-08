using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Player.Enums;
using Music_Player.Interfaces;

namespace Music_Player.Models {
  public static class TrackOptions {
    //todo: bidictionary
    public static Dictionary<TrackOption, string> OptionTexts = new Dictionary<TrackOption, string> {
      {TrackOption.PlayNext, "Play next"},
      {TrackOption.AddToQueue, "Add to queue" },
      {TrackOption.AddToEndOfQueue, "Add to end of queue"},
      {TrackOption.RemoveFromQueue, "Remove from queue"},
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

    public static async Task DisplayBasicOptionsAsync(ITrack track) {
      var dic = OptionTexts;
      var cancelString = "Cancel";

      var selectText = await App.Current.MainPage.DisplayActionSheet(track.ToString(), cancelString, null,
        dic[TrackOption.PlayNext],
        dic[TrackOption.AddToQueue],
        dic[TrackOption.AddToEndOfQueue]
        );

      if (selectText == null || selectText == cancelString)
        return;

      //todo: handle this with bidic instead
      ExecuteOption(dic.First(p => p.Value == selectText).Key, track);
    }

  }
}
