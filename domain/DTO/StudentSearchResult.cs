using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_and_repo.DTO
{
    public class StudentSearchResult
    {
            public string StudentName { get; set; }
        public List<CourseGrade> CourseGrades { get; set; }
            public int TotalDegrees { get; set; }
            public int Rank { get; set; }
        
    }

}

