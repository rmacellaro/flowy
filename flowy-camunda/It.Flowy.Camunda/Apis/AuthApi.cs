using System.Net.Http.Headers;
using It.Flowy.Camunda.Models.Auth;
using Newtonsoft.Json;

namespace It.Flowy.Camunda.Apis;

public interface IAuthApi {
  AuthenticationHeaderValue GetAuthenticationHeaderValue();
  void CheckAuthenticated();
  void Authenticate();
}

public class AuthApi : IAuthApi {

  public Token? CurrentToken { get; private set; }

  private readonly string BaseApiUrl = "http://localhost:18080";
  
  public AuthenticationHeaderValue GetAuthenticationHeaderValue() {
    CheckAuthenticated();
    if(CurrentToken == null) { throw new Exception("No Current Tooken");}
    if(CurrentToken.TokenType == null) { throw new Exception("No Current Tooken Type");}
    return new AuthenticationHeaderValue(CurrentToken.TokenType, CurrentToken.AccessToken);
  }

  public void CheckAuthenticated() {
    if (
      (CurrentToken != null) && 
      (CurrentToken.ExpiredDateTime != null) && 
      (CurrentToken.ExpiredDateTime > DateTime.Now)
    ) { return; }
    // mi devo ri autenticare perchè il token e scaduto o non ancora acquisito
    Authenticate();
  }

  public void Authenticate(){
    HttpClient client = new ();
    string url = BaseApiUrl + "/auth/realms/camunda-platform/protocol/openid-connect/token";
    var data = new[] {
      new KeyValuePair<string, string>("grant_type", "client_credentials"),
      new KeyValuePair<string, string>("client_id", "flowy"),
      new KeyValuePair<string, string>("client_secret", "kyzU2oR9YGYwiDhnxaAzRFExxBfQfIpy"),
      //new KeyValuePair<string, string>("audience", "operate.camunda.io")
    };
    Task<HttpResponseMessage> response = client.PostAsync(url, new FormUrlEncodedContent(data));
    string json = response.Result.Content.ReadAsStringAsync().Result;
    CurrentToken = JsonConvert.DeserializeObject<Token>(json);
  }
  
}
