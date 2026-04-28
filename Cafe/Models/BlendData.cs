namespace PulseLib.Cafe.Models;

using Newtonsoft.Json;

public struct BlendData
{
  [JsonProperty("blend")]
  public LevelData Blend { get; private set; }
}
