using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace Smooth.Client.Flaunt.Layout;

public partial class MainLayout
{
    private string _buildVersion = string.Empty;
    private string _frameWorkVersion = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        _buildVersion = GetBuildVersion();
        _frameWorkVersion = GetFrameworkVersion();
    }



    #region Helpers

    private string GetBuildVersion()
    {
        var buildVersion = typeof(Program).Assembly.GetName().Version;

        if (buildVersion is not null)
        { 
            return $"{buildVersion?.Major}.{buildVersion?.Minor}.{buildVersion?.Build}";
        }

        return string.Empty;
    }


    private string GetFrameworkVersion()
    {
        return Environment.Version.ToString();
    }

    #endregion Helpers
}
