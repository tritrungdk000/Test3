using Controller.Model;
using System.Reflection;

namespace Controller.Services
{
    public interface ICoursesServices
    {
     
        Task<List<Courses>> GetAllCourses();
        Task<Courses> GetIdCourses(Guid id, bool includeCourses = false);
        Task<Courses> AddCourses(Courses courses);
        Task<Courses> UpdateCourses(Courses courses);
        Task<(bool, string)> DeleteCourses(Courses courses);
        
        Task<List<Students>> getAllStudent();
        Task<Students> GetIdStudent(Guid id, bool includeCourses = false);
        Task<Students> AddStudent(Students students);
        Task<Students> UpdateStudent(Students students);
        Task<(bool, string)> DeleteStudent(Students students);

    }
}
