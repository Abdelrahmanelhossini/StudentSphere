using busnisslogic.content;
using busnisslogic.interfaces;
using domain_and_repo.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService _CourseService;

        public CoursesController(ICourseService CourseService)
        {
            _CourseService = CourseService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var courses = await _CourseService.GetAllStudentsAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var course = await _CourseService.GetStudentByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] Course course)
        {
            await _CourseService.AddStudentAsync(course);
            return CreatedAtAction(nameof(GetStudentById), new { id = course.CourseId}, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            await _CourseService.UpdateStudentAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _CourseService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}

