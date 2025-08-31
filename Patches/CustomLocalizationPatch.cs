namespace PulseLib.Patches;

[HarmonyPatch(typeof(LocalizationClient))]
internal static class CustomLocalizationPatch
{
  /*
  // We transpile this instead of patching LocalizationClient.ExistsLocalizedString as it is inlined.
  // As of Build 19593051, only GetWithCheck.GetString uses ExistsLocalizedString.
  [HarmonyPatch(typeof(RDString), nameof(RDString.GetWithCheck))]
  [HarmonyTranspiler]
  static IEnumerable<CodeInstruction> CustomLocalizationExistsPatch(
    IEnumerable<CodeInstruction> instructions
  )
  {
    CodeMatcher matcher = new(instructions);

    // Local functions have mangled names; we are matching this to find the local function GetString
    // `call bool RDString::'<GetWithCheck>g__GetString|29_0'(bool, string, valuetype RDString/'<>c__DisplayClass29_0'&)`
    while (true)
    {
      matcher.MatchForward(false, [
        new CodeMatch(OpCodes.Ldloca),
        new CodeMatch(OpCodes.Call),
      ]);

      if (matcher.Remaining == 0)
      {
        break;
      }

      matcher.Advance(1); // we should be at Call opcode after this
      matcher.Insert(
        CodeInstruction.Call(
          typeof(CustomLocalizationPatch),
          nameof(CustomLocalizationPatch),
          [
            typeof(bool),
            typeof(string),
          ]
          ))
    }

    return matcher.InstructionEnumeration();
  }

  public static bool CustomGetString(bool condition, string token)
  {
    // vanilla code
    if (condition && SA.GoogleDoc.Localization.Client.ExistsLocalizedString(token))
    {
      string value = SA.GoogleDoc.Localization.GetLocalizedString(token);
      return true;
    }
    return false;
  }

  // Naive patch if LocalizationClient.ExistsLocalizedString is not JIT inlined in the future
  // [HarmonyPatch(nameof(LocalizationClient.ExistsLocalizedString))]
  // [HarmonyPrefix]
  // static void CustomLocalizationExistsPatch(string token, ref bool __result, ref bool __runOriginal, ref LocalizationClient __instance)
  // {
  //   __result = __runOriginal = LocalizationHelpers.Exists(__instance.CurrentLanguage, token);
  //   __runOriginal = !__result;
  // }

  [HarmonyPatch(nameof(LocalizationClient.GetLocalizedString), [typeof(string), typeof(LangSection), typeof(LangCode)])]
  [HarmonyPrefix]
  static void GetLocalizedStringPatch(string token, LangSection section, LangCode language, ref string __result, ref bool __runOriginal)
  {
    Plugin.Logger.LogDebug("1");
    bool result = LocalizationHelpers.TryGetLocalizedString(language, token, out string localizedString);
    Plugin.Logger.LogDebug("2");

    if (!result)
    {
      Plugin.Logger.LogDebug("3");
      __runOriginal = true;
      Plugin.Logger.LogDebug("4");
      #if DEBUG
      Plugin.Logger.LogDebug($"Couldn't find {token} in {language}");
      #endif
      Plugin.Logger.LogDebug("5");
      return;
    }

    Plugin.Logger.LogDebug("6");
    __runOriginal = false;
    Plugin.Logger.LogDebug("7");
    __result = localizedString;
    Plugin.Logger.LogDebug("8");
    Plugin.Logger.LogDebug($"Got \"{localizedString}\" for {token}, {language}");
  }
  */
}
