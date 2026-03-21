using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PulseLib.Localization;

public static class CustomLocalizationHelper
{
  #region Registering custom localization
  //                         language           section            key     localized
  internal static Dictionary<string, Dictionary<string, Dictionary<string, string>>> customLocalizedStrings = new();

  /// <summary>
  /// Registers a localized string.
  /// </summary>
  /// <param name="language">Language to register under</param>
  /// <param name="section">Section of localization to register under</param>
  /// <param name="key">Localization ley</param>
  /// <param name="localized">Localized text</param>
  public static void Register(string language, string section, string key, string localized)
  {
    if (!customLocalizedStrings.ContainsKey(language))
    {
      Plugin.Logger.LogDebug($"[Localization] Creating language dictionary for {language}");
      customLocalizedStrings[language] = new Dictionary<string, Dictionary<string, string>>();
    }
    if (!customLocalizedStrings[language].ContainsKey(section))
    {
      Plugin.Logger.LogDebug($"[Localization] Creating section dictionary for {section} of {language}");
      customLocalizedStrings[language][section] = new Dictionary<string, string>();
    }
    customLocalizedStrings[language][section][key] = localized;

    Plugin.Logger.LogDebug($"[Localization] Registered '{section}.{key}' as '{localized}' for {language}");
  }

  /// <summary>
  /// Registers a language dictionary under the section provided.
  /// </summary>
  /// <param name="language">Language to register under.</param>
  /// <param name="section">Section of localization to register under.</param>
  /// <param name="localized">Language dictionary.</param>
  public static void Register(string language, string section, Dictionary<string, string> localized)
  {
    foreach ((string key, string localizedTo) in localized)
    {
      Register(language, section, key, localizedTo);
    }
  }

  public static void Register(LangCode language, LangSection section, Dictionary<string, string> localized)
    => Register(language.ToString(), section.ToString(), localized);

  public static void Register(string language, LangSection section, Dictionary<string, string> localized)
    => Register(language, section.ToString(), localized);

  public static void Register(Dictionary<LangCode, Dictionary<LangSection, Dictionary<string, string>>> localized)
  {
    foreach ((LangCode language, Dictionary<LangSection, Dictionary<string, string>> content) in localized)
    {
      foreach ((LangSection section, Dictionary<string, string> innerLocalized) in content)
      {
        Register(language, section, innerLocalized);
      }
    }
  }

  public static void Register(Dictionary<string, Dictionary<string, Dictionary<string, string>>> localized)
  {
    foreach ((string language, Dictionary<string, Dictionary<string, string>> content) in localized)
    {
      foreach ((string section, Dictionary<string, string> innerLocalized) in content)
      {
        Register(language, section, innerLocalized);
      }
    }
  }

  /// <summary>
  /// Registers an amalgamated language dictionary into the custom localization system.
  /// </summary>
  /// <param name="language">Language to register under.</param>
  /// <param name="localized">Language dictionary.</param>
  public static void Register(string language, Dictionary<string, Dictionary<string, string>> localized)
  {
    foreach ((string section, Dictionary<string, string> innerLocalized) in localized)
    {
      Register(language, section, innerLocalized);
    }
  }

  /// <summary>
  /// Searches the given path for JSON files to add to the custom localization system.
  /// </summary>
  /// <param name="path">Path to search under</param>
  public static void SearchAndRegisterDirectory(string path)
  {
    Plugin.Logger.LogDebug($"[Localization] Searching for localization JSONs in {path}");

    IEnumerable<string> paths = Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
    // TODO: Remove
    // foreach (string language in paths)
    // {
    //   Directory.GetDirectories(language, "*", SearchOption.TopDirectoryOnly);
    // }

    foreach (string languagePath in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
    {
      // Directory.EnumerateDirectories returns full paths, we get the actual folder name here
      RegisterDirectory(path, new DirectoryInfo(languagePath).Name);
    }
  }

  /// <summary>
  /// Searches directory for localization JSONs under the custom localization system.
  /// </summary>
  /// <param name="path">Path to search under</param>
  /// <param name="language">Language to add under</param>
  public static void RegisterDirectory(string path, string language)
  {
    // TODO: Make more flexible - if flat, assume filename as section setup.
    //       If has nested dicts, treat as amalgamated setup

    Plugin.Logger.LogDebug($"[Localization] Registering localization JSONs in {path} for {language}");

    IEnumerable<string> fileNames = Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories);
    if (fileNames.Count() == 1)
    {
      // Amalgamated setup
      Register(language, (Dictionary<string, Dictionary<string, string>>)Json.Deserialize(File.ReadAllText(fileNames.ElementAt(0))));
      return;
    }

    foreach (string fileName in Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories))
    {
      // Filename as section setup
      Register(language, Path.GetFileNameWithoutExtension(fileName), JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(fileName))!);
    }
  }
  #endregion

  public static string GetLanguage()
  {
    if (string.IsNullOrWhiteSpace(Configuration.CustomLanguage.Value))
    {
      return SA.GoogleDoc.Localization.Client.CurrentLanguage.ToString();
    }

    return Configuration.CustomLanguage.Value;
  }
}
