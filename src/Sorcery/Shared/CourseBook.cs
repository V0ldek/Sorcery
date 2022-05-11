﻿// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.
using Sorcery.ModularCourse;
using Sorcery.ModularCourse.Skeletons;

namespace Sorcery.Shared;

public sealed class CourseBook
{
    private readonly ICourseFactory _courseFactory;

    public IReadOnlyList<Course> Courses { get; }

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
                }, new AssignmentSkeleton ("Lustrous Loot", "lustrous-loot", "https://classroom.github.com/a/UdxGZ4xr")),
                new ("Generics and Collections", "generics-and-collections", new SectionSkeleton[]
                {
                    new ("Indexers", "indexers"),
                    new ("Generics", "generics"),
                    new ("Equality", "equality"),
                    new ("Ordering", "ordering"),
                    new ("Explicit Interface Implementations", "explicit-interface-implementations"),
                    new ("Collections", "collections"),
                    new ("Comparers", "comparers"),
                    new ("Tuples", "tuples"),
                    new ("Deconstruction", "deconstruction"),
                    new ("Nested Types", "nested-types"),
                    new ("HashCode as a Mutable Struct", "hashcode-as-a-mutable-struct"),
                }, new AssignmentSkeleton("Diverging Dungeons", "diverging-dungeons", "https://classroom.github.com/a/EuwNc18U")),
                new ("LINQ", "linq", new SectionSkeleton[]
                {
                    new ("Generic Variance", "generic-variance"),
                    new ("Iterators", "iterators"),
                    new ("Extension Methods", "extension-methods"),
                    new ("Anonymous Types", "anonymous-types"),
                    new ("Delegates", "delegates"),
                    new ("Lambda Expressions", "lambda-expressions"),
                    new ("LINQ Queries", "linq-queries"),
                    new ("Local Methods", "local-methods"),
                }, new AssignmentSkeleton("Layered Layouts", "layered-layouts", "https://classroom.github.com/a/wJNxPTTZ")),
                new ("Asynchrony", "asynchrony", new SectionSkeleton[]
                {
                    new ("Simple Threading", "simple-threading"),
                    new ("Events", "events"),
                    new ("Exception Handling", "exception-handling"),
                    new ("Disposable Resources", "disposable-resources"),
                    new ("Async and Await", "async-and-await"),
                    new ("Thread Pool", "thread-pool"),
                    new ("Cancellation", "cancellation"),
                    new ("Async Interfaces and ValueTask", "async-interfaces-and-valuetask"),
                }, new AssignmentSkeleton("Persisted Pathways", "persisted-pathways", "https://classroom.github.com/a/OsaBB_jJ")),
                new ("Entity Framework", "entity-framework", new SectionSkeleton[]
                {
                    new ("Expression Trees", "expression-trees"),
                    new ("LINQ to SQL", "linq-to-sql"),
                    new ("Navigation Properties", "navigation-properties"),
                }, new AssignmentSkeleton("", "", "https://gienieczko.com/todo") { IsHidden = true },
                new ("Concurrency", "concurrency", new SectionSkeleton[]
                {
                    new ("PLINQ", "plinq"),
                    new ("Concurrent Collections", "concurrent-collections"),
                    new ("Blocking Collections", "blocking-collections"),
                }, new AssignmentSkeleton("Concurrent Competitivity", "concurrent-competitivity", "https://gienieczko.com/todo"))
                {
                    IsHidden = true
                },
                new ("Analysers", "analysers", new SectionSkeleton[]
                {
                    new ("Nullable Static Analysis", "nullable-static-analysis"),
                }, new AssignmentSkeleton("Advanced Adventure Analysis", "advanced-adventure-analysis", "https://gienieczko.com/todo"))
                {
                    IsHidden = true
                },
            }));
}
