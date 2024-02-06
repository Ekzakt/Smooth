using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;

namespace Smooth.Client.Flaunt.Pages;

public partial class Cors
{
    [Inject]
    public IHttpDataManager _dataManager { get; set; }

    private string _getRandomGuidResult = string.Empty;
    private string _postRandomGuid = Guid.NewGuid().ToString();
    private string _postRandomGuidResult = string.Empty;


    private async Task GetRandomGuid()
    {
        var result = await _dataManager.GetDataAsync<string>(EndPoints.GET_RANDOM_GUID(), true);

        _getRandomGuidResult = result ??= Guid.Empty.ToString();
    }


    private async Task PostRandomGuid()
    {
        var request = new PostRandomGuidRequest { Value = new Guid(_postRandomGuid) };

        var result = await _dataManager.PostDataAsync<PostRandomGuidRequest, PostRandomGuidResponse>(EndPoints.POST_GUID(request.Value), request, true);

        _postRandomGuidResult = result!.Value.ToString();
    }
}
