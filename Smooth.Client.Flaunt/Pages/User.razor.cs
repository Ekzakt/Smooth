using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Smooth.Client.Flaunt.Pages;

public partial class User : ComponentBase
{
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }

    private IEnumerable<Claim>? claims;

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (authenticationState is not null && authenticationState.User is not null)
        {
            claims = authenticationState.User.Claims ?? new List<Claim>();
        }
    }
}



