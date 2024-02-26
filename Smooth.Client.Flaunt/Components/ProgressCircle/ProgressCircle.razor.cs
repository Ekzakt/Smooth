using Microsoft.AspNetCore.Components;

namespace Smooth.Client.Flaunt.Components.ProgressCircle;

public partial class ProgressCircle
{
    private float _progress = 30;
    private float _circumference = 251.2f;
    private string _dasharray = "0";
    private float _offset;

    [Parameter]
    public float Progress
    {
        get => _progress;
        set
        {
            _progress = value;
            UpdateProgress();
        }
    }



    private void UpdateProgress()
    {
        _offset = _circumference - (_progress / 100) * _circumference;
        _dasharray = $"{_progress} {_circumference}";
    }
}
