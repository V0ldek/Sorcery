// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse;

public sealed record class Section(Module Module, int Id, string Title, string RouteName)
{
    public Section? Next { get; internal set; }

    public Section? Previous { get; internal set; }

    public string DisplayName => $"{Module.Id}.{Id}. {Title}";

    public string Route => RouteHelper.BuildRoute(Module.Route, Id, RouteName);
}
