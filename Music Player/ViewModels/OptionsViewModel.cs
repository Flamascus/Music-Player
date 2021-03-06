using Music_Player.Interfaces;
using Music_Player.Models;
using Music_Player.Services;

namespace Music_Player.ViewModels {
  public class OptionsViewModel {

    private readonly ITrack _track;

    public OptionsViewModel(ITrack track) {
      this._track = track;
    }

    public void AddNext() => TrackQueue.Instance.NextUpTracks.Insert(0, this._track);
    public void AddToQueue() => TrackQueue.Instance.NextUpTracks.Add(this._track);

  }
}
