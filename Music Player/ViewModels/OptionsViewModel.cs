using Music_Player.Interfaces;
using Music_Player.Models;
namespace Music_Player.ViewModels {
  public class OptionsViewModel {

    private readonly ITrack _track;

    public OptionsViewModel(ITrack track) {
      this._track = track;
    }

    public void AddNext() => TrackQueue.Instance.AddNext(this._track);
    public void AddToQueue() => TrackQueue.Instance.AddToQueue(this._track);

  }
}
