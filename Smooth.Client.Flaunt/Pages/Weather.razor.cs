using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.WeaterForcasts;
using Smooth.Client.Flaunt.HttpClients;
using Smooth.Shared.Endpoints;
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
    public int? R { get; set; } = 10;


    private List<WeatherForecastResponseDto>? forecasts = new();


    protected override async Task OnInitializedAsync()
    {
        await GetWeartherForecasts();
    }



    #region Helpers

    private async Task GetWeartherForecasts()
    {
        var endpoint = WeatherForecastEndpoints.GET_BY_ROWCOUNT(R);

        var result = await _httpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);

        if (result is not null)
        {
            forecasts = result;
        }

    }

    #endregion Helpers
}
