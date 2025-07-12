namespace PulseLib;

public class Events
{
  #region Singleton pattern
  private static readonly Lazy<Events> _lazy = new(() => new Events());

  // ReSharper disable once MemberCanBePrivate.Global
  public static Events Instance => _lazy.Value;

  private Events()
  {
    Plugin.Logger.LogInfo("Events singleton created");
  }
  #endregion

  #region Events
  /// <summary>
  /// Invoked when a level on the level select (story mode) was deselected.
  /// </summary>
  public event EventHandler? LevelDeselected;

  /// <summary>
  /// Internally used by <see cref="EventsLink.LinkRaiseLevelDeselectedEventPatch"/>.
  /// </summary>
  public static void RaiseLevelDeselected()
  {
#if DEBUG
    Plugin.Logger.LogDebug("RaiseLevelDeselected");
#endif
    Instance.LevelDeselected?.Invoke(null, EventArgs.Empty);
  }
  #endregion
}
