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
    public class levelController : Controller
    {
        private readonly ILevelServce _levelSevice;

        public levelController(ILevelServce levelServce)
        {
            _levelSevice = levelServce;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var levels = await _levelSevice.GetAllStudentsAsync();
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var level = await _levelSevice.GetStudentByIdAsync(id);
            if (level == null)
            {
                return NotFound();
            }
            return Ok(level);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] Level level)
        {
            await _levelSevice.AddStudentAsync(level);
            return CreatedAtAction(nameof(GetStudentById), new { id = level.LevelId }, level);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Level level)
        {
            if (id != level.LevelId)
            {
                return BadRequest();
            }

            await _levelSevice.UpdateStudentAsync(level);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _levelSevice.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}

