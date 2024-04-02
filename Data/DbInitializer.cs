using Microsoft.EntityFrameworkCore;
using Controller.Model;

namespace Controller.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<Students>(s =>
            {
                s.HasData(new Students
                {
                    StudentId = new Guid("e2397972-8743-431a-9482-60292f08320e"),
                    Name = "Nguyen van a"
                });
                s.HasData(new Students
                {
                    StudentId = new Guid("4e79044a-988d-4488-97b7-3e474e4340d2"),
                    Name = "Nguyen van b"
                });
            });
            _builder.Entity<Courses>(c =>
            {
                c.HasData(new Courses
                {
                    CourseId = new Guid("9250d994-2558-4573-8465-417248667051"),
                    CourseName = "AAA",
                    Description = "abc"
                }); ;
                c.HasData(new Courses
                {
                    CourseId = new Guid("88738493-3a85-4443-8f6a-313453432192"),
                    CourseName = "Lap Trinh Web",
                    Description = "tach tach",
                });
            });
            _builder.Entity<StudentCourses>(b =>
            {
                b.HasData(new StudentCourses
                {
                    StudentId = new Guid("e2397972-8743-431a-9482-60292f08320e"),
                    CoursesId = new Guid("88738493-3a85-4443-8f6a-313453432192")
                });
                b.HasData(new StudentCourses
                {
                    StudentId = new Guid("4e79044a-988d-4488-97b7-3e474e4340d2"),
                    CoursesId = new Guid("9250d994-2558-4573-8465-417248667051")
                });
            });
        }
    }
}