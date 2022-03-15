// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.ModularCourse;

public record class Module(Course Course, int Id, string Title, string RouteName)
{
    private readonly List<Section> _sections = new();

    public bool IsHidden { get; init; }

    public Assignment? Assignment { get; internal set; }

    public string DisplayName => $"{Id}. {Title}";

    public Module? Next { get; internal set; }

    public Module? Previous { get; internal set; }

    public string Route => RouteHelper.BuildRoute(Course.Route, Id, RouteName);

    public IReadOnlyList<Section> Sections => _sections;

    public Section this[string sectionRoute] => Sections.FirstOrDefault(m => m.RouteName == sectionRoute) ??
            throw new ArgumentException($"Did not find section {sectionRoute} in module {RouteName}", nameof(sectionRoute));

    internal void AddSection(Section section) => _sections.Add(section);
}
