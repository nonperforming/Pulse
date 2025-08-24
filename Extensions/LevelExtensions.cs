namespace PulseLib.Extensions;

/// <summary>
/// Extensions for the level enum.
/// Lists do not include cut/unreleased content.
/// </summary>
public static class LevelExtensions
{
  /// <summary>
  /// All levels that have a rank (not boss levels).
  /// Includes bonus levels such as Beans Hopper and Rhythm Weightlifter.
  /// </summary>
  public static readonly Level[] AllStandardLevels = [
    // Act 1
    Level.OrientalTechno,
    Level.OrientalDubstep,
    Level.Intimate,
    Level.IntimateH,
    Level.GongXi,
    Level.Halloween,
    // Act 2
    Level.Lofi,
    Level.CareLess,
    Level.SVT,
    Level.Unreachable,
    Level.Smokin,
    Level.Pomeranian,
    Level.SongOfTheSea,
    Level.SongOfTheSeaH,
    // Act 3
    Level.Garden,
    Level.Lounge,
    Level.Classy,
    Level.ClassyH,
    Level.DistantDuet,
    Level.DistantDuetH,
    // Act 4
    Level.Heldbeats,
    Level.Rollerdisco,
    Level.Invisible,
    Level.InvisibleH,
    Level.Steinway,
    Level.SteinwayH,
    Level.KnowYou,
    Level.Murmurs,
    // Act 5
    Level.LuckyBreak,
    Level.Injury,
    Level.Freezeshot,
    Level.FreezeshotH,
    Level.AthleteTherapy,
    Level.RhythmWeightlifter,
    // Bonus
    Level.ArtExercise,
    Level.HelpingHands,
    Level.VividStasis,
    Level.SparkLine,
    Level.Unbeatable,
    Level.MeetAndTweet,
    Level.BlackestLuxuryCar,
    Level.TapeStopNight,
    Level.The90sDecision,
  ];

  /// <summary>
  /// All boss levels.
  /// </summary>
  public static readonly Level[] AllBossLevels = [
    Level.OrientalInsomniac,
    Level.Boss2,
    Level.Lesmis,
    Level.InsomniacHard,
    Level.AthleteFinale,
  ];

  /// <summary>
  /// All bonus levels.
  /// </summary>
  public static readonly Level[] AllBonusLevels = [
    Level.BeansHopper,
    Level.RhythmWeightlifter,
  ];

  /// <summary>
  /// All intermission levels.
  /// </summary>
  public static readonly Level[] AllIntermissionLevels = [
    Level.SongOfTheSea,
    Level.SongOfTheSeaH,
    Level.AthleteTherapy,
  ];
}
