
using Microsoft.EntityFrameworkCore;
using Controller.Data;
using Controller.Model;
using Controller.Services;
using REST_API_TEMPLATE.Data;

namespace Controller.Services
{
    public class CoursesServices:ICoursesServices
    {
        private readonly AppDbContext _context;
        private ICoursesServices _coursesServicesImplementation;
        public CoursesServices(AppDbContext context) { _context = context; }

        #region Courses

        public async Task<List<Courses>> GetAllCourses()
        {
            try
            {
                return await _context.Courses.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Courses> GetIdCourses(Guid id, bool includeCourses)
        {
            try
            {
                if (includeCourses)
                {
                    return await _context.Courses.Include(c => c.StudentCourses).FirstOrDefaultAsync(i => i.CourseId == id);
                }

                return await _context.Courses.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Courses> AddCourses(Courses courses)
        {
            try
            {
                await _context.Courses.AddAsync(courses);
                await _context.SaveChangesAsync();
                return await _context.Courses.FindAsync(courses.CourseId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Courses> UpdateCourses(Courses courses)
        {
            try
            {
                _context.Entry(courses).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return courses;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCourses(Courses courses)
        {
            try
            {
                var dbCourses = await _context.Courses.FindAsync(courses.CourseId);
                if (dbCourses == null)
                {
                    return (false, "Not Found");
                }

                _context.Courses.Remove(courses);
                return (true, "Success");
            }
            catch (Exception e)
            {
                return (false, "Failed");
            }
        }



        #endregion

        #region Student
        public Task<List<Students>> getAllStudent()
        {
            return _coursesServicesImplementation.getAllStudent();
        }
        public async Task<List<Students>> GetAllStudent()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Students> GetIdStudent(Guid id, bool includeCourses)
        {
            try
            {
                if (includeCourses)
                {
                    return await _context.Students.Include(c => c.StudentCourses).FirstOrDefaultAsync(i => i.StudentId== id);
                }

                return await _context.Students.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Students> AddStudent(Students student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return await _context.Students.FindAsync(student.StudentId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Students> UpdateStudent(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return student;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteStudent(Students student)
        {
            try
            {
                var dbStudent = await _context.Students.FindAsync(student);
                if (dbStudent == null)
                {
                    return (false, "Courses could not be found");
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return (true, "Amzing good job you");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        #endregion

        #region StudentCourese

        public async Task<List<Courses>> GetAllSCourses()
        {
            try
            {
                return await _context.Courses.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Model.Courses> GetIdSCourses(Guid id)
        {
            try
            {
                return await _context.Courses.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Model.Courses> AddSCourses(Model.Courses sc)
        {
            try
            {
                await _context.Courses.AddAsync(sc);
                await _context.SaveChangesAsync();

                return await _context.Courses.FindAsync(sc.CourseId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Model.Courses> UpdateSCourses(Model.Courses sc)
        {
            try
            {
                _context.Entry(sc).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return sc;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSCourses(Model.Courses sc)
        {
            try
            {
                var dbSC = await _context.Courses.FindAsync(sc.CourseId);
                if (dbSC == null)
                {
                    return (false, "Courses could not be found");
                }
                _context.Courses.Remove(sc);
                await _context.SaveChangesAsync();
                return (true, "Amazing good job you");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        #endregion
    }
}
