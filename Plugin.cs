using System.IO;

namespace PulseLib;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
  // ReSharper disable once NullableWarningSuppressionIsUsed
  internal static new ManualLogSource Logger = null!;

  private readonly string _localizationPath = Path.Join(Paths.GameRootPath, "Localization");

  private Harmony _harmony = new(MyPluginInfo.PLUGIN_GUID);

  private void Awake()
  {
    Logger = base.Logger;
    Logger.LogInfo($"Loading {MyPluginInfo.PLUGIN_NAME} version {MyPluginInfo.PLUGIN_VERSION}");

    Setup();

    Logger.LogInfo("Binding configuration");
    Configuration.Bind(Config);

    if (Configuration.LoadLocalizationFromDisk.Value)
    {
      Logger.LogInfo("Discovering localization");
      CustomLocalizationHelper.SearchAndRegisterDirectory(_localizationPath);
    }

    Logger.LogInfo("Creating links to events");
    _harmony.PatchAll(typeof(EventsLink));

    Logger.LogInfo("Applying patches");
    _harmony.PatchAll(typeof(CustomLocalizationPatch));
    _harmony.PatchAll(typeof(MainMenuPatch));

    Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} loaded!");
  }

  private void Setup()
  {
    Directory.CreateDirectory(_localizationPath);
  }
}
