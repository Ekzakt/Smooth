using Microsoft.AspNetCore.Components;

namespace Smooth.Client.Flaunt.Components.ProgressBar;

public partial class ProgressBar
{
    [Parameter]
    public double Value { get; set; } = 0;

    private string GetWidthStyle => $"width: {Value}%;";

}
