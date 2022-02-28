// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.
using Sorcery.ModularCourse;
using Sorcery.ModularCourse.Skeletons;

namespace Sorcery.Shared;

public sealed class CourseBook
{
    private readonly ICourseFactory _courseFactory;

    public IReadOnlyList<Course> Courses;

    public CourseBook(ICourseFactory courseFactory)
    {
        _courseFactory = courseFactory;

        Courses = new[]
        {
            CSharpCourse
        };
    }

    public Course CSharpCourse => _courseFactory.FromSkeleton(
        new("C#.NET Course", "csharp", new ModuleSkeleton[]
            {
                new ("Basics", "basics", new SectionSkeleton[]
                {
                }, null)
            }));
}
