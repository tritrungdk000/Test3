using Microsoft.AspNetCore.Mvc;
using Controller.Model;
using Controller.Services;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class studentsController : ControllerBase
    {
        private readonly ICoursesServices _coursesService;

        public studentsController(ICoursesServices coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        public async Task<IActionResult> Getstudents()
        {
            var students = await _coursesService.getAllStudent();
            if (students == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No students in database.");
            }

            return StatusCode(StatusCodes.Status200OK, students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getstudents(Guid id)
        {
            Students students = await _coursesService.GetIdStudent(id);

            if (students == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No students found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, students);
        }

        [HttpPost]
        public async Task<ActionResult<Students>> Addstudents(Students students)
        {
            var dbstudents = await _coursesService.AddStudent(students);

            if (dbstudents == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"students could not be added.");
            }

            return CreatedAtAction(nameof(Getstudents), new { id = students.StudentId }, students);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatestudents(Guid id, Students students)
        {
            if (id != students.StudentId)
            {
                return BadRequest();
            }

            Students dbstudents = await _coursesService.UpdateStudent(students);

            if (dbstudents == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"student could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestudents(Guid id)
        {
            var students = await _coursesService.GetIdStudent(id);
            (bool status, string message) = await _coursesService.DeleteStudent(students);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, students);
        }
    }
}