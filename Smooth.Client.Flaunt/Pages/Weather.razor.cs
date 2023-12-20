using Ekzakt.TaskAuditor;
using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Endpoints;
using Smooth.Client.Application.WeaterForcasts;
using Smooth.Client.Flaunt.HttpClients;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Weather
{
    [Inject]
    public FunctionAppApiHttpClient _functionAppHttpClient { get; set; }

    [Inject]
    public WebAppApiHttpClient _webAppHttpClient { get; set; }

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
    private string? elapsedTime1;
    private string? elapsedTime2;

    private long elapsedMs1;
    private long elapsedMs2;

    public async Task GetWeartherForecasts()
    {
        var endpoint = WeatherForecastEndpoints.GET_BY_ROWCOUNT(R);
        var taskAuditor = new TaskAuditor<List<WeatherForecastResponseDto>?>();

        Task<List<WeatherForecastResponseDto>?> task1 = _functionAppHttpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);
        Task<List<WeatherForecastResponseDto>?> task2 = _webAppHttpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);

        var sw = new Stopwatch();

        if (task1 is not null)
        {
            sw.Start();

            var result = await _functionAppHttpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);

            if (result is not null)
            {
                forecasts = result;
            }

            sw.Stop();

            elapsedMs1 = sw.ElapsedMilliseconds;
            elapsedTime1 = sw.Elapsed.ToString();
        }


        if (task2 is not null)
        {
            sw.Restart();

            var result = await _webAppHttpClient.Client.GetFromJsonAsync<List<WeatherForecastResponseDto>>(endpoint);

            if (result is not null)
            {
                forecasts?.AddRange(result);
            }

            sw.Stop();

            elapsedMs2 = sw.ElapsedMilliseconds;
            elapsedTime2 = sw.Elapsed.ToString();
        }
    }
}
