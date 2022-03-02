// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Sorcery.ModularCourse.Skeletons;

namespace Sorcery.ModularCourse;

public sealed class CourseFactory : ICourseFactory
{
    public Course FromSkeleton(CourseSkeleton skeleton)
    {
        var course = new Course(skeleton.DisplayName, skeleton.RouteName);
        Module? previousModule = null;

        foreach (var moduleSkeleton in skeleton.Modules)
        {
            var module = new Module(course, course.Modules.Count, moduleSkeleton.DisplayName, moduleSkeleton.RouteName);
            course.AddModule(module);

            if (previousModule is not null)
            {
                previousModule.Next = module;
                module.Previous = previousModule;
            }

            previousModule = module;

            Section? previousSection = null;

            foreach (var sectionSkeleton in moduleSkeleton.Sections)
            {
                var section = new Section(module, module.Sections.Count, sectionSkeleton.DisplayName, sectionSkeleton.RouteName);
                module.AddSection(section);

                if (previousSection is not null)
                {
                    previousSection.Next = section;
                    section.Previous = previousSection;
                }

                previousSection = section;
            }

            if (moduleSkeleton.Assignment is not null)
            {
                var assignment = new Assignment(
                    module,
                    moduleSkeleton.Assignment.DisplayName,
                    moduleSkeleton.Assignment.RouteName,
                    moduleSkeleton.Assignment.GitHubClassroom);
                module.Assignment = assignment;
            }
        }

        return course;
    }
}
