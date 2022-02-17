// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Sorcery.ModularCourse.Skeletons;

namespace Sorcery.ModularCourse;

public interface ICourseFactory
{
    Course FromSkeleton(CourseSkeleton skeleton);
}
