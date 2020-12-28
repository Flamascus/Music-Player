namespace Music_Player.Models {

  public enum ResultType {
    Artist,
    Genre,
    Song,
  }

  public class SearchResult {

    public string Text { get; }
    public string Description { get; }

  }
}
