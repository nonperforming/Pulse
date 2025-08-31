using System.IO;
using Newtonsoft.Json;

namespace PulseLib.Localization;

public static class LocalizationHelpers
{
  private static Dictionary<LangCode, Dictionary<string, string>> Data = new();

  public static bool Exists(LangCode language, string key) => TryGetLocalizedString(language, key, out _);

  public static bool TryGetLocalizedString(LangCode language, string key, out string value)
  {
    value = string.Empty;
    if (!Data.TryGetValue(language, out Dictionary<string, string> localizedStrings))
    {
      return false;
    }

    if (!localizedStrings.TryGetValue(key, out string localizedString))
    {
      return false;
    }

    value = localizedString;
    return true;
  }

  public static void Register(LangCode language, KeyValuePair<string, string> localizedString) =>
    Register(language, localizedString.Key, localizedString.Value);

  public static void Register(LangCode language, string key, string value)
  {
    Plugin.Logger.LogDebug($"Registering {{{key}: {value}}} for {language}");
    if (!Data.ContainsKey(language))
    {
      Data[language] = new();
    }
    Data[language].Add(key, value);
  }

  public static void RegisterJson(LangCode language, string data)
  {
    Plugin.Logger.LogDebug($"Registering {data} for {language}");
    Dictionary<string, Dictionary<string, string>> values =
      JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(data)
      ?? throw new ArgumentNullException();
    foreach (KeyValuePair<string, string> localizedString in values["Translations"])
    {
      Register(language, localizedString);
    }
  }

  public static void LoadAndRegisterJson(LangCode language, string path) =>
    RegisterJson(language, File.ReadAllText(path));
}
