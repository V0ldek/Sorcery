// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse;

public sealed record class Course(string Title, string RouteName)
{
    private readonly List<Module> _modules = new();

    public IReadOnlyList<Module> Modules => _modules;

    public string Route => RouteHelper.BuildRoute("/teaching", RouteName);

    public string DisplayName => Title;

    public Module this[string moduleRoute] => Modules.First(m => m.RouteName == moduleRoute);

    internal void AddModule(Module module) => _modules.Add(module);
}
