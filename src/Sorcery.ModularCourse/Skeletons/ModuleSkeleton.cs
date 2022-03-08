// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse.Skeletons;

public sealed record class ModuleSkeleton(string DisplayName, string RouteName, IReadOnlyList<SectionSkeleton> Sections, AssignmentSkeleton? Assignment)
{
    public bool IsHidden { get; init; }
}
