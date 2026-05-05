using System.IO;

namespace PulseLib.Extensions;

public static class scnBaseExtensions
{
  public static void GoToLevelWithCustomLevelData(CustomLevelData levelData, bool twoPlayer) =>
    scnBase.GoToLevelWithExternalPath(
      twoPlayer
      && !levelData.isLegacyLevel
      && levelData.settings.canBePlayedOn == LevelPlayMode.BothModes
      && !string.IsNullOrEmpty(levelData.settings.separate2PLevelFilename)
        ? Path.Combine(levelData.path, levelData.settings.separate2PLevelFilename)
        : Path.Combine(levelData.path, "main.rdlevel")
    );

  public static void GoToLevelWithImportLevel(ImportLevel importLevel, bool twoPlayer) =>
    scnBase.GoToLevelWithExternalPath(
      twoPlayer
      && importLevel.settings.canBePlayedOn == LevelPlayMode.BothModes
      && !string.IsNullOrEmpty(importLevel.settings.separate2PLevelFilename)
        ? Path.Combine(importLevel.path, importLevel.settings.separate2PLevelFilename)
        : Path.Combine(importLevel.path, "main.rdlevel")
    );
}
