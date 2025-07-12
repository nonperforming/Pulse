using PulseLib.Patches;

namespace PulseLib;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
  // ReSharper disable once NullableWarningSuppressionIsUsed
  internal static new ManualLogSource Logger = null!;

  private Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

  private void Awake()
  {
    Logger = base.Logger;
    Logger.LogInfo($"Loading {MyPluginInfo.PLUGIN_NAME} version {MyPluginInfo.PLUGIN_VERSION}");

    Logger.LogInfo("Creating links to events");
    _harmony.PatchAll(typeof(EventsLink));

    Logger.LogInfo("Applying patches");
    _harmony.PatchAll(typeof(MainMenuPatch));

    Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} loaded!");
  }
}
