namespace Smooth.Shared.Endpoints;

public static class TestEndpoints
{
    public const string CONTROLLER = "/api/test";

    public static string INSERT_TEST_CLASS() => $"{CONTROLLER}/testclass";
    public static string INSERT_SEND_EMAIL() => $"{CONTROLLER}/email";
}
