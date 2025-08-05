// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Sorcery.Utilities;

namespace Sorcery.Blogging;

public record class Post(string Title, string RouteName, DateTime? DateOfPublication)
{
    public IReadOnlyList<Tag> Tags { get; init; } = Array.Empty<Tag>();

    public string Route => RouteHelper.BuildRoute("/sourcery", RouteName);

    public required RenderFragment Description { get; init; }

    public required string ShortDescription { get; init; }

    public Series? Series { get; private set; }

    [MemberNotNullWhen(true, nameof(DateOfPublication))]
    public bool IsPublished => DateOfPublication is not null;

    private int? _idxInSeries;

    public int? NumberInSeries => _idxInSeries + 1;

    public Post? NextInSeries =>
        _idxInSeries.HasValue ? Series?.Posts.ElementAtOrDefault(_idxInSeries.Value + 1) : null;

    public Post? PreviousInSeries =>
        _idxInSeries is > 0 ? Series?.Posts.ElementAt(_idxInSeries.Value - 1) : null;

    internal void AddToSeries(Series series, int idxInSeries)
    {
        Series = series;
        _idxInSeries = idxInSeries;
    }
}
