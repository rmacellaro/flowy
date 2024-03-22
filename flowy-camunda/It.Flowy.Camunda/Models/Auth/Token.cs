using Newtonsoft.Json;

namespace It.Flowy.Camunda.Models.Auth;

public class Token { 

  [JsonProperty("access_token")]
  public string? AccessToken { get; set; }

  [JsonProperty("token_type")]
  public string? TokenType { get; set; }

  [JsonProperty("expires_in")]
  public int ExpiresIn { get; set; }

  [JsonProperty("refresh_expires_in")]
  public int RefreshExpiresIn { get; set; }

  [JsonProperty("scope")]
  public string? Scope { get; set; }
  
  public DateTime? ExpiredDateTime { get; set; }
}