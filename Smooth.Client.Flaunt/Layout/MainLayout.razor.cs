using Smooth.Shared.Configurations;

namespace Smooth.Client.Flaunt.Layout;

public partial class MainLayout
{
    private AppVersions? _appVersions;


    protected override void OnInitialized()
    {
        var assemblyVersion = typeof(Program).Assembly?.GetName()?.Version;
        _appVersions = new AppVersions(assemblyVersion!, Environment.Version);
    }
}
