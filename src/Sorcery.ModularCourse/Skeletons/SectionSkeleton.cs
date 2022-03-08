// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse.Skeletons;

public sealed record class SectionSkeleton(string DisplayName, string RouteName)
{
    public bool IsHidden { get; init; }
}
