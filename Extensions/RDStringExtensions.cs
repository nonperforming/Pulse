namespace PulseLib.Extensions;

public static class RDStringExtensions
{
  public static string GetWithSubstitution(string key, params KeyValuePair<string, string>[] substituteWith)
  {
    string str = RDString.Get(key);

    foreach ((string k, string v) in substituteWith)
    {
      str = str.Replace($"[{k}]", v);
    }

    return str;
  }
}
