using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PulseLib.Cafe;

public static class Cafe
{
  public const string URL = "https://rhythm.cafe/";
  public const string API_URL = URL + "api/";

  private static HttpClient _client = new()
  {
    BaseAddress = new Uri(API_URL),
    DefaultRequestHeaders =
    {
      Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") },
      UserAgent = { ProductInfoHeaderValue.Parse($"Pulse/{MyPluginInfo.PLUGIN_VERSION}") },
    },
  };

  public static async Task<LevelData?> GetLevelData(string levelId)
  {
    using HttpResponseMessage response = await _client.GetAsync($"levels/{levelId}/");
    if (!response.IsSuccessStatusCode)
    {
      Plugin.Logger.LogError($"[{nameof(Cafe)}] Could not get level data: {response.ReasonPhrase}");
      return null;
    }

    return JsonConvert.DeserializeObject<LevelData>(await response.Content.ReadAsStringAsync());
  }

  public static async Task<BlendData?> GetDailyBlend()
  {
    using HttpResponseMessage response = await _client.GetAsync("levels/todays-blend/");
    if (!response.IsSuccessStatusCode)
    {
      Plugin.Logger.LogError($"[{nameof(Cafe)}] Could not get daily blend: {response.ReasonPhrase}");
      return null;
    }

    return JsonConvert.DeserializeObject<BlendData>(await response.Content.ReadAsStringAsync());
  }
}
