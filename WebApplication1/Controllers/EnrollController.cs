using busnisslogic.interfaces;
using domain_and_repo.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollController : Controller
    {
        private readonly IEnrollServece _enrollSevice;

        public EnrollController(IEnrollServece enrollServece)
        {
            _enrollSevice = enrollServece;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var enrolls = await _enrollSevice.GetAllStudentsAsync();
            return Ok(enrolls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var enroll = await _enrollSevice.GetStudentByIdAsync(id);
            if (enroll == null)
            {
                return NotFound();
            }
            return Ok(enroll);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] Enroll enroll)
        {
            await _enrollSevice.AddStudentAsync(enroll);
            return CreatedAtAction(nameof(GetStudentById), new { id = enroll.EnrollId }, enroll);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Enroll enroll)
        {
            if (id !=enroll.EnrollId)
            {
                return BadRequest();
            }

            await _enrollSevice.UpdateStudentAsync(enroll);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _enrollSevice.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}

