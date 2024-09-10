using domain_and_repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busnisslogic.interfaces
{
    public interface IStudentSearchService
    {

        List<StudentSearchResult> SearchStudents(string studentName, int? level = null, int? minDegrees = null, int? maxDegrees = null, int? minRank = null, int? maxRank = null);

    }
       

    
}
