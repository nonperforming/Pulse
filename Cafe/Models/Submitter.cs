namespace PulseLib.Cafe.Models;

using Newtonsoft.Json;

public struct Submitter
{
  [JsonProperty("id")]
  public string Id { get; private set; }

  [JsonProperty("displayName")]
  public string DisplayName { get; private set; }
}
