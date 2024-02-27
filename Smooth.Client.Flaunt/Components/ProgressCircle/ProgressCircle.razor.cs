using Microsoft.AspNetCore.Components;

namespace Smooth.Client.Flaunt.Components.ProgressCircle;

public partial class ProgressCircle
{
    private int progressPercent = 0;
    private double dashOffset;

    protected override async Task OnInitializedAsync()
    {
        // Example: Start updating progress every second
        var timer = new System.Timers.Timer(25);
        timer.Elapsed += async (sender, e) => await UpdateProgress();
        timer.Start();
    }

    private async Task UpdateProgress()
    {
        // Simulate progress update
        progressPercent = (progressPercent + 1) % 101;
        dashOffset = 50 - (50 * progressPercent / 100); // Adjusted dash offset
        await InvokeAsync(StateHasChanged);
    }
}
