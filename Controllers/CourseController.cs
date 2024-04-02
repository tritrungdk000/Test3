using Controller.Model;
using Controller.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICoursesServices _coursesService;


        public CourseController(ICoursesServices coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var Courses = await _coursesService.GetAllCourses();

            if (Courses == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Courses  in database");
            }

            return StatusCode(StatusCodes.Status200OK, Courses);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCourses(Guid id, bool includeBooks = false)
        {
            Courses Courses = await _coursesService.GetIdCourses(id, includeBooks);

            if (Courses  == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Courses  found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, Courses);
        }

        [HttpPost]
        public async Task<ActionResult<Courses>> AddCourses(Courses Courses)
        {
            var dbCourses = await _coursesService.AddCourses(Courses);

            if (dbCourses  == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{Courses.CourseName} could not be added.");
            }

            return CreatedAtAction("GetCourses ", new { id = Courses.CourseId }, Courses);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateCourses(Guid id, Courses Courses)
        {
            if (id != Courses.CourseId)
            {
                return BadRequest();
            }

            Courses dbCourses = await _coursesService.UpdateCourses(Courses);

            if (dbCourses  == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{Courses.CourseName} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCourses(Guid id)
        {
            var Courses = await _coursesService.GetIdCourses(id, false);
            (bool status, string message) = await _coursesService.DeleteCourses(Courses);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Courses);
        }
    }
}
