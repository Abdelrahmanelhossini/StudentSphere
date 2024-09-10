using busnisslogic.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class Search : Controller
    {



        private readonly IStudentSearchService _studentSearchService;

        public Search(IStudentSearchService studentSearchService)
        {
            _studentSearchService = studentSearchService;
        }

        [HttpGet("search")]
        public IActionResult Searchh(string? studentName, int? level = null, int? minDegrees = null, int? maxDegrees = null, int? minRank = null, int? maxRank = null) { 

      
        
            var result = _studentSearchService.SearchStudents(studentName,level,minDegrees,maxDegrees,minRank,maxRank);
            return Ok(result);
        }
    }
}


