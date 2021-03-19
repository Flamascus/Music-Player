using Music_Player.Helpers;
using Music_Player.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Music_Player.Models {

  public class TrackList : LoadableList<Track> {

    public static TrackList Instance = new TrackList();
    private TrackList() { }

  }
}
