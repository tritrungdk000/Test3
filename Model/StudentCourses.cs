using System.ComponentModel.DataAnnotations;

namespace Controller.Model
{
    public class StudentCourses
    {
        public Students? Students { get; set; }
        public Guid? StudentId { get; set; }
        public Courses? Courses { get; set; }
        public Guid? CoursesId { get; set; }
    }
}
