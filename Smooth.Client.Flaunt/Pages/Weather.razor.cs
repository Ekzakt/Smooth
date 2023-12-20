using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Endpoints;
using Smooth.Client.Application.WeaterForcasts;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Weather
{
    [Inject]
    public ApiHttpClient _httpClient { get; set; }

    [Inject]
    public NavigationManager _navigationManager { get; set; }

    [Inject]
    public IConfiguration _configuration { get; set; }

    [Parameter]
    public int? R { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await GetWeartherForecasts();
    }

    private List<WeatherForecastResponseDto>? forecasts = new();
    private string? elapsedTime;
    private long elapsedMs;

    public async Task GetWeartherForecasts()
    {
        var endpoint = WeatherForecastEndpoints.GET_BY_ROWCOUNT(R);

        var sw = new Stopwatch();
        sw.Start();

        var result = await _httpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);

        if (result is not null)
        {
            forecasts = result;
        }

        sw.Stop();

        elapsedMs = sw.ElapsedMilliseconds;
        elapsedTime = sw.Elapsed.ToString();
    }
}
