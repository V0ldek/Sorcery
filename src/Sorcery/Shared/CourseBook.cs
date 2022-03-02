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
                    new (".NET Taxonomy", "dotnet-taxonomy"),
                    new ("Configuring Your Environment", "configuring-your-environment"),
                    new ("Hello, World!", "hello-world"),
                    new ("Basic Types", "basic-types"),
                    new ("Control Flow", "control-flow"),
                    new ("Arrays", "arrays"),
                    new ("Classes", "classes"),
                    new ("Inheritance", "inheritance"),
                    new ("Abstract Types", "abstract-types"),
                    new ("Strings", "strings"),
                    new ("Attributes", "attributes"),
                    new ("Testing with xUnit", "testing-with-xunit"),
                    new ("Using GitHub Classroom", "using-github-classroom"),
                }, null)
            }));
}
