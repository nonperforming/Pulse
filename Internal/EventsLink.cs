namespace PulseLib.Internal;

[HarmonyPatch]
internal static class EventsLink
{
  /// <summary>
  /// Link <see cref="Events.LevelDeselected"/>
  /// </summary>
  /// <param name="instructions">IL instructions</param>
  /// <returns>Modified instructions that calls <see cref="Events.RaiseLevelDeselected"/> before any action is taken.</returns>
  [HarmonyPatch(typeof(scnLevelSelect), nameof(scnLevelSelect.Update))]
  [HarmonyTranspiler]
  private static IEnumerable<CodeInstruction> LinkRaiseLevelDeselectedEventPatch(
    IEnumerable<CodeInstruction> instructions)
  {
    return new CodeMatcher(instructions)
      .MatchForward(true,
        // Checking for RDInput.cancelPress
        new CodeMatch(OpCodes.Ldsfld, AccessTools.Field(typeof(RDInput), nameof(RDInput.cancelPress))),
        new CodeMatch(OpCodes.Brfalse), // jump out early if false
        // && heartMonitor.visible && !heartMonitor.goingToLevel
        new CodeMatch(OpCodes.Ldarg_0),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(scnLevelSelect), nameof(scnLevelSelect.heartMonitor))),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(HeartMonitor), nameof(HeartMonitor.visible))),
        new CodeMatch(OpCodes.Brfalse_S)
      )
      .Insert(CodeInstruction.Call(typeof(Events), nameof(Events.RaiseLevelDeselected)))
      .InstructionEnumeration();
  }
}
