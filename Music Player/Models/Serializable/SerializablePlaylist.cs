using Music_Player.Models.DisplayGroup;
using System.Linq;

namespace Music_Player.Models.Serializable {
  public class SerializablePlaylist {

    public string Name { get; }
    public string[] TrackPaths { get; }

    public SerializablePlaylist(string name, string[] trackPaths) {
      this.Name = name;
      this.TrackPaths = trackPaths;      
    }

    public static SerializablePlaylist FromPlaylist(Playlist playlist)
      => new SerializablePlaylist(playlist.Name, playlist.Tracks.Select(t => t.Path).ToArray());

    public Playlist ToPlaylist() => new Playlist(this.Name, Helpers.Helpers.CreateTracklistFromPaths(this.TrackPaths));
  }
}