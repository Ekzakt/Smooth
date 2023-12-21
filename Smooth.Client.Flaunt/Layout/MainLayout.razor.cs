
using System.Reflection;

namespace Smooth.Client.Flaunt.Layout;

public partial class MainLayout
{
    private string version = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var fullVersion = typeof(Program).Assembly.GetName().Version;

        version = $"{fullVersion?.Major}.{fullVersion?.Minor}.{fullVersion?.Build}";   

    }
}
