using BepInEx.Configuration;

namespace PulseLib;

internal static class Configuration
{
  // Localization
  internal static ConfigEntry<bool> UseCustomLocalization = null!;
  internal static ConfigEntry<bool> LoadLocalizationFromDisk = null!;
  internal static ConfigEntry<string> CustomLanguage = null!;

  // Development
  //internal static ConfigEntry<bool> ShowLocalizationKeys = null!; // TODO: A better option would be a 'None' language type. / Also add note for turning on narration.
  internal static ConfigEntry<bool> LogLocalization = null!;
  internal static ConfigEntry<bool> DumpLocalizationDictionary = null!;

  internal static void Bind(ConfigFile config)
  {
    // Localization
    UseCustomLocalization = config.Bind(
      "Localization",
      nameof(UseCustomLocalization),
      true,
      "Whether to activate Pulse's localization system or not."
    );
    LoadLocalizationFromDisk = config.Bind(
      "Localization",
      nameof(LoadLocalizationFromDisk),
      true,
      "Load localization files from disk.\n" +
      "Localization files should be put under `<Game Dir>/Localization/<Language name>/<section>.json`."
    );
    CustomLanguage = config.Bind<string>(
      "Localization",
      nameof(CustomLanguage),
      "",
      "The custom language chosen."
    );

    // Development
    //ShowLocalizationKeys = config.Bind(
    //  "Development",
    //  nameof(ShowLocalizationKeys),
    //  false,
    //  "Do not localize any strings, instead show their localization key."
    //);
    LogLocalization = config.Bind(
      "Development",
      nameof(LogLocalization),
      false,
      "Log any calls that attempt to get a localized string."
    );
    DumpLocalizationDictionary = config.Bind(
      "Development",
      nameof(DumpLocalizationDictionary),
      false,
      "Dump any loaded localization dictionary to disk.\n" +
      "Dumped files will be found under `<Game Dir>/Dumped/<Language name>.json`."
    );
  }
}
