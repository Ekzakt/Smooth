using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Smooth.Client.Flaunt.Pages;

public partial class User : ComponentBase
{
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }

    [Inject]
    public IAccessTokenProvider _tokenProvider { get; set; }


    private IEnumerable<Claim>? claims;
    private string _jwtToken = "No token found.";



    protected override async Task OnInitializedAsync()
    {
        await SetClaims();
        await SetJwtToken();
    }




    #region Helpers

    private async Task SetJwtToken()
    {
        if (_tokenProvider is not null)
        {
            return;
        }

        var tokenResult = await _tokenProvider.RequestAccessToken();

        if (tokenResult is null)
        {
            return;
        }

        if (tokenResult.TryGetToken(out var token))
        {
            _jwtToken = token?.Value;
        }
    }


    private async Task SetClaims()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (authenticationState is not null && authenticationState.User is not null)
        {
            claims = authenticationState.User.Claims ?? new List<Claim>();
        }
    }


    #endregion Helpers
}



