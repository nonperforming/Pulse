namespace Pulse;

public class Events
{
  #region Singleton pattern
  private static readonly Lazy<Events> _lazy = new(() => new Events());
  public static Events Instance => _lazy.Value;
  #endregion

  #region Events
  /// <summary>
  /// Invoked when a level on the level select (story mode) was deselected.
  /// </summary>
  public event EventHandler LevelDeselected;

  /// <summary>
  /// Internally used by
  /// </summary>
  public static void RaiseLevelDeselected()
  {
    Instance.LevelDeselected.Invoke(null, null);
  }
  #endregion
}
