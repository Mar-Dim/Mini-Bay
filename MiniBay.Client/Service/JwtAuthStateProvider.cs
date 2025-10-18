using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly LocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly AuthenticationState _anonymous;

    public JwtAuthStateProvider(LocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync("authToken");
        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymous;
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

   public void NotifyUserLogout()
{
    var authState = Task.FromResult(_anonymous);
    _http.DefaultRequestHeaders.Authorization = null; 
    NotifyAuthenticationStateChanged(authState);
}

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Name, out var name);
            if (name != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, name.ToString()!));
            }
            // Añade más claims si los necesitas (email, roles, etc.)
        }
        
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}