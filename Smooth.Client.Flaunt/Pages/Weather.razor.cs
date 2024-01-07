using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.HttpClients;
using Smooth.Shared.Dtos;
using Smooth.Shared.Endpoints;
using System.Net.Http.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Weather : IDisposable
{

    [Inject]
    public ApiHttpClient _httpClient { get; set; }

    [Inject]
    public NavigationManager _navigationManager { get; set; }

    [Inject]
    public IConfiguration _configuration { get; set; }

    [Parameter]
    public int? R { get; set; } = 10;


    private CancellationTokenSource cancellationToken = new();
    private List<WeatherForecastResponseDto>? forecasts = new();


    protected override async Task OnInitializedAsync()
    {
        await GetWeartherForecasts();
    }



    #region Helpers

    private async Task GetWeartherForecasts()
    {
        var endpoint = WeatherForecastEndpoints.GET_BY_ROWCOUNT(R);

        var result = await _httpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint, cancellationToken.Token);

        if (result is not null)
        {
            forecasts = result;
        }

    }


    void IDisposable.Dispose()
    {
        cancellationToken.Cancel();
        cancellationToken.Dispose();
    }

    #endregion Helpers
}
