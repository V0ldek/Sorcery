// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Sorcery.ModularCourse.Skeletons;

namespace Sorcery.ModularCourse;

public sealed class CourseFactory : ICourseFactory
{
    public Course FromSkeleton(CourseSkeleton skeleton)
    {
        var course = new Course(skeleton.DisplayName, skeleton.RouteName);

        foreach (var moduleSkeleton in skeleton.Modules)
        {
            AddModuleToCourse(moduleSkeleton, course);
        }

        return course;
    }

    private static void AddModuleToCourse(ModuleSkeleton moduleSkeleton, Course course)
    {
        var moduleId = course.Modules.Count;
        var module = new Module(course, moduleId, moduleSkeleton.DisplayName, moduleSkeleton.RouteName)
        {
            IsHidden = moduleSkeleton.IsHidden
        };

        if (moduleId > 0)
        {
            var previousModule = course.Modules[^1];
            previousModule.Next = module;
            module.Previous = previousModule;
        }

        course.AddModule(module);

        foreach (var sectionSkeleton in moduleSkeleton.Sections)
        {
            AddSectionToModule(sectionSkeleton, module);
        }

        if (moduleSkeleton.Assignment is not null)
        {
            AddAssignmentToModule(moduleSkeleton.Assignment, module);
        }
    }

    private static void AddSectionToModule(SectionSkeleton sectionSkeleton, Module module)
    {
        var sectionId = module.Sections.Count;
        var section = new Section(module, sectionId, sectionSkeleton.DisplayName, sectionSkeleton.RouteName)
        {
            IsHidden = sectionSkeleton.IsHidden
        };

        if (sectionId > 0)
        {
            var previousSection = module.Sections[^1];
            previousSection.Next = section;
            section.Previous = previousSection;
        }

        module.AddSection(section);
    }

    private static void AddAssignmentToModule(AssignmentSkeleton assignmentSkeleton, Module module)
    {
        var assignment = new Assignment(
            module,
            assignmentSkeleton.DisplayName,
            assignmentSkeleton.RouteName,
            assignmentSkeleton.GitHubClassroom)
        {
            IsHidden = assignmentSkeleton.IsHidden
        };
        module.Assignment = assignment;
    }
}
