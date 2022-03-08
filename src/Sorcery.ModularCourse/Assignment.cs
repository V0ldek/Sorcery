// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse;

public sealed record class Assignment(Module Module, string Title, string RouteName, string GitHubClassroom)
{
    public bool IsHidden { get; init; }

    public string DisplayName => $"Assignment {Module.Id}. – {Title}";

    public string Route => RouteHelper.BuildRoute($"{Module.Route}/assignment", Module.Id, RouteName);
}
