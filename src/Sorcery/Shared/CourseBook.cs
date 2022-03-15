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
                }, null),
                new ("Object Orientation", "object-orientation", new SectionSkeleton[]
                {
                    new ("Classes", "classes"),
                    new ("Inheritance", "inheritance"),
                    new ("Abstract Types", "abstract-types"),
                    new ("Strings", "strings"),
                    new ("Attributes", "attributes"),
                    new ("Testing with xUnit", "testing-with-xunit"),
                    new ("Using GitHub Classroom", "using-github-classroom"),
                }, new AssignmentSkeleton ("Dungeon Walker", "dungeon-walker", "https://classroom.github.com/a/7S1aHBvK")),
                new ("References and Values", "references-and-values", new SectionSkeleton[]
                {
                    new ("Memory", "memory"),
                    new ("Reference Types and Value Types", "reference-types-and-value-types"),
                    new ("Operator Overloading", "operator-overloading"),
                    new ("Exceptions", "exceptions"),
                    new ("Nullability", "nullability"),
                    new ("Casting", "casting"),
                    new ("Pass-by-Reference", "pass-by-reference"),
                }, new AssignmentSkeleton ("Lustrous Loot", "lustrous-loot", "https://gienieczko.com/todo")),
                new ("Generics and Records", "generics-and-records", new SectionSkeleton[]
                {
                    new ("Generics", "generics"),
                    new ("Desctructuring", "desctructuring"),
                    new ("Tuples", "tuples"),
                    new ("Equality", "equality"),
                    new ("Records", "records"),
                    new ("Pattern Matching", "pattern-matching"),
                    new ("Collections", "collections"),
                }, new AssignmentSkeleton("Superior Strategies", "superior-strategies", "https://gienieczko.com/todo")),
                new ("LINQ", "linq", new SectionSkeleton[]
                {
                    new ("Nested Types", "nested-types"),
                    new ("Extension Methods", "extension-methods"),
                    new ("Anonymous Types", "anonymous-types"),
                    new ("Iterators", "iterators"),
                    new ("Delegates", "delegates"),
                    new ("Lambda Expressions", "lambda-expressions"),
                    new ("LINQ queries", "linq-queries"),
                    new ("Expression API", "expression-api"),
                }, new AssignmentSkeleton("Darkest Dungeons", "darkest-dungeons", "https://gienieczko.com/todo")),
                new ("Asynchrony", "asynchrony", new SectionSkeleton[]
                {
                    new ("Task Parallel Library", "task-parallel-library")
                }, new AssignmentSkeleton("Asynchronous Adventurers", "asynchronous-adventurers", "https://gienieczko.com/todo")),
                new ("Concurrency", "concurrency", new SectionSkeleton[]
                {
                    new ("Events", "events"),
                }, new AssignmentSkeleton("Concurrent Competitivity", "concurrent-competitivity", "https://gienieczko.com/todo")),
                new ("Analysers", "analysers", new SectionSkeleton[]
                {
                    new ("Nullable Static Analysis", "nullable-static-analysis"),
                }, new AssignmentSkeleton("Advanced Adventure Analysis", "advanced-adventure-analysis", "https://gienieczko.com/todo"))
            }));
}
