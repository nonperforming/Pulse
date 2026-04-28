namespace PulseLib.Cafe.Models;

using Newtonsoft.Json;

public struct Club
{
  [JsonProperty("id")]
  public string Id { get; private set; }

  [JsonProperty("name")]
  public string Name { get; private set; }
}
