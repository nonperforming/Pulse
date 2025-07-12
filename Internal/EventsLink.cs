namespace PulseLib.Internal;

[HarmonyPatch]
internal static class EventsLink
{
  /// <summary>
  /// Link <see cref="Events.LevelDeselected"/>
  /// </summary>
  /// <param name="instructions">IL instructions</param>
  /// <returns>
  /// Modified instructions that calls <see cref="Events.RaiseLevelDeselected"/> before any action is taken.
  /// </returns>
  /// <seealso cref="Events.LevelDeselected"/>
  /// <seealso cref="Events.RaiseLevelDeselected"/>
  [HarmonyPatch(typeof(scnLevelSelect), nameof(scnLevelSelect.Update))]
  [HarmonyTranspiler]
  private static IEnumerable<CodeInstruction> LinkRaiseLevelDeselectedEventPatch(
    IEnumerable<CodeInstruction> instructions
  )
  {
    return new CodeMatcher(instructions)
      .MatchForward(
        true,
        // RDInput.cancelPress
        new CodeMatch(OpCodes.Ldsfld, AccessTools.Field(typeof(RDInput), nameof(RDInput.cancelPress))),
        new CodeMatch(OpCodes.Brfalse), // jump out early if false (should be true)
        // && heartMonitor.visible
        new CodeMatch(OpCodes.Ldarg_0),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(scnLevelSelect), nameof(scnLevelSelect.heartMonitor))),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(HeartMonitor), nameof(HeartMonitor.visible))),
        new CodeMatch(OpCodes.Brfalse), // jump out early if false (should be true)
        // && !heartMonitor.goingToLevel
        new CodeMatch(OpCodes.Ldarg_0),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(scnLevelSelect), nameof(scnLevelSelect.heartMonitor))),
        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(HeartMonitor), nameof(HeartMonitor.goingToLevel))),
        new CodeMatch(OpCodes.Brtrue) // jump out early if true (should be false)
      )
      .ThrowIfInvalid(
        "Could not find RDInput.cancelPress && heartMonitor.visible && !heartMonitor.goingToLevel in scnLevelSelect.Update"
      )
      .InsertAndAdvance(CodeInstruction.Call(typeof(Events), nameof(Events.RaiseLevelDeselected)))
      .InstructionEnumeration();
  }
}
