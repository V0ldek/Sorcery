// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.
using Microsoft.AspNetCore.Components;
using Sorcery.Utilities;

namespace Sorcery.Blogging;

public record class Post(string Title, string RouteName, DateTime? DateOfPublication)
{
    public IReadOnlyList<Tag> Tags { get; init; } = Array.Empty<Tag>();

    public string Route => RouteHelper.BuildRoute("/sourcery", RouteName);

    public required RenderFragment Description { get; init; }

    public required string ShortDescription { get; init; }
}
