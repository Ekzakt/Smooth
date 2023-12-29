using Microsoft.AspNetCore.Components;

namespace Smooth.Client.Flaunt.Components;

public partial class Article
{
    [Parameter]
    public string Title { get; set; } = string.Empty;


    [Parameter]
    public string Value { get; set; } = string.Empty;
}
