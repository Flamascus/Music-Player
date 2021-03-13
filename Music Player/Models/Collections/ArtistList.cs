using Music_Player.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Music_Player.Models {
  public class ArtistList : LoadableList<Artist> {

    public static ArtistList Instance = new ArtistList();
    private ArtistList() { }

  }
}
