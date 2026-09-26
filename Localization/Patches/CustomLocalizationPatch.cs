using System.Diagnostics.CodeAnalysis;

namespace PulseLib.Localization.Patches;

[HarmonyPatch(typeof(LocalizationClient))]
internal static class CustomLocalizationPatch
{
  [HarmonyPatch(nameof(LocalizationClient.ExistsLocalizedString))]
  [HarmonyPrefix]
  private static void CheckCustomLocalizationExists(string token, ref bool __result, ref bool __runOriginal)
  {
    if (TryGetCustomLocalization(token, out string? _))
    {
      __result = true;
      __runOriginal = false;
    }
  }

  // TODO: also implement for any section as string, i.e. mod wants custom section
  // TODO: add fallback language option
  [HarmonyPatch(nameof(LocalizationClient.GetLocalizedString), typeof(string), typeof(LangSection), typeof(LangCode))]
  [HarmonyPrefix]
  private static void GetCustomLocalization(
    string token,
    LangCode language,
    ref string __result,
    ref bool __runOriginal
  )
  {
    if (TryGetCustomLocalization(token, out string? localized))
    {
      __result = localized;
      __runOriginal = false;

      Plugin.Logger.LogDebug($"Got custom localization '{token}' as '{localized}'");
    }
  }

  private static bool TryGetCustomLocalization(string token, [NotNullWhen(true)] out string? localized)
  {
    string[] splitting = token.Split('.');
    localized = null;

    if (
      !CustomLocalizationHelper.customLocalizedStrings.TryGetValue(
        CustomLocalizationHelper.GetLanguage(),
        out Dictionary<string, Dictionary<string, string>> langDict
      )
    )
    {
      return false;
    }

    for (int sectionLength = splitting.Length - 1; sectionLength > 0; sectionLength--)
    {
      string section = string.Join(".", splitting, 0, sectionLength);
      string key = string.Join(".", splitting, sectionLength, splitting.Length - sectionLength);

      if (
        langDict.TryGetValue(section, out Dictionary<string, string> inSection)
        && inSection.TryGetValue(key, out localized)
      )
      {
        return true;
      }
    }

    return false;
  }

  private static bool TryGetCustomLocalization(string section, string key, [NotNullWhen(true)] out string? localized)
  {
    // TODO: Implement custom language support

    if (
      !CustomLocalizationHelper.customLocalizedStrings.TryGetValue(
        CustomLocalizationHelper.GetLanguage(),
        out Dictionary<string, Dictionary<string, string>> langDict
      ) || !langDict.TryGetValue(section, out Dictionary<string, string> inSection)
    )
    {
      localized = null;
      return false;
    }

    return inSection.TryGetValue(key, out localized);
  }
}
