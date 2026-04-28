namespace PulseLib.Cafe.Models;

using Newtonsoft.Json;

public struct LevelData
{
  [JsonProperty("id")]
  public string Id { get; private set; }

  [JsonProperty("artist")]
  public string Artist { get; private set; }

  [JsonProperty("artist_tokens")]
  public string[] ArtistTokens { get; private set; }

  [JsonProperty("artist_raw")]
  public string ArtistRaw { get; private set; }

  [JsonProperty("song")]
  public string Song { get; private set; }

  [JsonProperty("song_alt")]
  public string SongAlt { get; private set; }

  [JsonProperty("song_raw")]
  public string SongRaw { get; private set; }

  [JsonProperty("seizure_warning")]
  public bool SeizureWarning { get; private set; }

  [JsonProperty("description")]
  public string Description { get; private set; }

  [JsonProperty("hue")]
  public float Hue { get; private set; }

  [JsonProperty("authors")]
  public string[] Authors { get; private set; }

  [JsonProperty("authors_raw")]
  public string AuthorsRaw { get; private set; }

  [JsonProperty("max_bpm")]
  public long MaxBPM { get; private set; }

  [JsonProperty("min_bpm")]
  public long MinBPM { get; private set; }

  [JsonProperty("difficulty")]
  public LevelDifficulty Difficulty { get; private set; }

  [JsonProperty("single_player")]
  public bool SinglePlayer { get; private set; }

  [JsonProperty("two_player")]
  public bool TwoPlayer { get; private set; }

  [JsonProperty("last_updated")]
  public DateTimeOffset LastUpdated { get; private set; }

  [JsonProperty("tags")]
  public string[] Tags { get; private set; }

  [JsonProperty("sha1")]
  public string SHA1 { get; private set; }

  [JsonProperty("rdlevel_sha1")]
  public string RDLevelSHA1 { get; private set; }

  [JsonProperty("rd_md5")]
  public string RDMD5 { get; private set; }

  [JsonProperty("is_animated")]
  public bool IsAnimated { get; private set; }

  [JsonProperty("rdzip_url")]
  public Uri RDZipUrl { get; private set; }

  [JsonProperty("image_url")]
  public Uri ImageUrl { get; private set; }

  [JsonProperty("thumb_url")]
  public Uri ThumbUrl { get; private set; }

  [JsonProperty("icon_url")]
  public string IconUrl { get; private set; }

  [JsonProperty("submitter")]
  public Submitter Submitter { get; private set; }

  [JsonProperty("club")]
  public Club Club { get; private set; }

  [JsonProperty("approval")]
  public int Approval { get; private set; }

  [JsonProperty("approval_notes_public")]
  public string ApprovalNotesPublic { get; private set; }

  [JsonProperty("is_private")]
  public bool IsPrivate { get; private set; }

  [JsonProperty("prefill_version")]
  public long PrefillVersion { get; private set; }

  [JsonProperty("has_classics")]
  public bool HasClassics { get; private set; }

  [JsonProperty("has_oneshots")]
  public bool HasOneshots { get; private set; }

  [JsonProperty("has_squareshots")]
  public bool HasSquareshots { get; private set; }

  [JsonProperty("has_freezeshots")]
  public bool HasFreezeshots { get; private set; }

  [JsonProperty("has_burnshots")]
  public bool HasBurnshots { get; private set; }

  [JsonProperty("has_holdshots")]
  public bool HasHoldshots { get; private set; }

  [JsonProperty("has_triangleshots")]
  public bool HasTriangleshots { get; private set; }

  [JsonProperty("has_skipshots")]
  public bool HasSkipshots { get; private set; }

  [JsonProperty("has_subdivs")]
  public bool HasSubdivs { get; private set; }

  [JsonProperty("has_synco")]
  public bool HasSynco { get; private set; }

  [JsonProperty("has_freetimes")]
  public bool HasFreetimes { get; private set; }

  [JsonProperty("has_holds")]
  public bool HasHolds { get; private set; }

  [JsonProperty("has_window_dance")]
  public bool HasWindowDance { get; private set; }

  [JsonProperty("has_rdcode")]
  public bool HasRDCode { get; private set; }

  [JsonProperty("has_cpu_rows")]
  public bool HasCpuRows { get; private set; }

  [JsonProperty("total_hits_approx")]
  public long TotalHitsApprox { get; private set; }

  [JsonProperty("is_hidden")]
  public bool IsHidden { get; private set; }
}
