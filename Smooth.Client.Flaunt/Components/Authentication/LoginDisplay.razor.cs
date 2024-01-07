using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Smooth.Client.Flaunt.Components.Authentication;

public partial class LoginDisplay
{
    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }
}
