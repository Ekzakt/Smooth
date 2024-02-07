using Smooth.Shared.Models.Requests;
using System.Web;

namespace Smooth.Shared.Endpoints;

public static class EndPoints
{

    // Test 
    public static string INSERT_TESTCLASS() => $"{Ctrls.TIAL}/{Routes.POST_TEXTCLASS}";
    public static string TRIGGER_EMAIL() => $"{Ctrls.TIAL}/{Routes.POST_TRIGGER_EMAIL}";
    public static string GET_RANDOM_GUID() => $"{Ctrls.TIAL}/{Routes.GET_RANDOM_GUID}";
    public static string POST_GUID(Guid value) => $"{Ctrls.TIAL}/{Routes.POST_GUID}?value={value}";


    // WeatherForecasts 
    public static string GET_WEATHERFORECASTS(int? rowCount) => $"{Ctrls.WEATERFORECASTS}?r={rowCount ?? 10}";


    // Configuration
    public static string GET_MEDIAFILES_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_MEDIAFILES_OPTIONS}";
    public static string GET_IMAGE_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_IMAGE_OPTIONS}";
    public static string GET_VIDEO_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_VIDEO_OPTIONS}";
    public static string GET_SOUND_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_SOUND_OPTIONS}";
    public static string GET_AZURE_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_AZURE_OPTIONS}";
    public static string GET_CORS_OPTIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_CORS_OPTIONS}";
    public static string GET_APP_VERSIONS() => $"{Ctrls.CONFIGURATION}/{Routes.GET_APP_VERSIONS}";


    // Files
    public static string GET_FILES_LIST() => $"{Ctrls.FILES}/{Routes.GET_FILES_LIST}";
    public static string DELETE_FILE(DeleteFileRequest request) => $"{Ctrls.FILES}/{Routes.DELETE_FILE}?filename={HttpUtility.UrlEncode(request.FileName)}";
}
