// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse.Skeletons;

public sealed record class CourseSkeleton(string DisplayName, string RouteName, IReadOnlyList<ModuleSkeleton> Modules);
